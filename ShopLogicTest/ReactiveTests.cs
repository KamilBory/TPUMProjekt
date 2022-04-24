using Microsoft.VisualStudio.TestTools.UnitTesting;

using Data = ShopData.Interface;
using Impl = ShopLogic.Basic;

using ShopLogic.Interface;

using System;
using System.Threading;
using System.Collections.Generic;

namespace ShopLogicTest
{
    [TestClass]
    public class ReactiveTests
    {
        private ILogic _logic;
        private IClientLogic _clientLogic;

        [TestMethod]
        public void ReactiveWorkflow_SingleObserver()
        {
            Queue<Offer> queue = new Queue<Offer>();

            var observer = new TestObserver(queue);

            var unsubscriber = _clientLogic.SubscribeForOfferUpdate(observer);

            observer.WaitForNotification();

            int offerId;
            int price;

            lock (queue)
            {
                var offer = queue.Dequeue();
                offerId = offer.id;
                price = offer.sellPrice;
            }

            observer.WaitForNotification();

            lock (queue)
            {
                var offer = queue.Dequeue();
                Assert.AreEqual(offerId, offer.id);
                Assert.AreEqual(price + 1, offer.sellPrice);
                offerId = offer.id;
                price = offer.sellPrice;
            }

            observer.WaitForNotification();

            lock (queue)
            {
                var offer = queue.Dequeue();
                Assert.AreEqual(offerId, offer.id);
                Assert.AreEqual(price + 1, offer.sellPrice);
            }

            unsubscriber.Dispose();

            _logic.Shutdown();
        }

        [TestMethod]
        public void ReactiveWorkflow_MultipleObservers()
        {
            Queue<Offer> queue1 = new Queue<Offer>();
            Queue<Offer> queue2 = new Queue<Offer>();

            var observer1 = new TestObserver(queue1);
            var observer2 = new TestObserver(queue2);

            var unsubscriber1 = _clientLogic.SubscribeForOfferUpdate(observer1);
            var unsubscriber2 = _clientLogic.SubscribeForOfferUpdate(observer2);

            observer1.WaitForNotification();
            observer2.WaitForNotification();

            int offerId;
            int price;

            lock (queue1)
            {
                var offer = queue1.Dequeue();
                offerId = offer.id;
                price = offer.sellPrice;
            }

            lock (queue2)
            {
                var offer = queue2.Dequeue();
                Assert.AreEqual(offer.id, offerId);
                Assert.AreEqual(offer.sellPrice, price);
            }

            observer1.WaitForNotification();
            observer2.WaitForNotification();

            lock (queue1)
            {
                var offer = queue1.Dequeue();
                Assert.AreEqual(offerId, offer.id);
                Assert.AreEqual(price + 1, offer.sellPrice);
                offerId = offer.id;
                price = offer.sellPrice;
            }

            lock (queue2)
            {
                var offer = queue2.Dequeue();
                Assert.AreEqual(offer.id, offerId);
                Assert.AreEqual(offer.sellPrice, price);
            }

            observer1.WaitForNotification();
            observer2.WaitForNotification();

            lock (queue1)
            {
                var offer = queue1.Dequeue();
                Assert.AreEqual(offerId, offer.id);
                Assert.AreEqual(price + 1, offer.sellPrice);
                offerId = offer.id;
                price = offer.sellPrice;
            }

            lock (queue2)
            {
                var offer = queue2.Dequeue();
                Assert.AreEqual(offer.id, offerId);
                Assert.AreEqual(offer.sellPrice, price);
            }

            unsubscriber1.Dispose();
            unsubscriber2.Dispose();

            _logic.Shutdown();
        }

        public class TestObserver : IObserver<Offer>
        {
            private Queue<Offer> _updatedOffers;
            private AutoResetEvent _notified = new AutoResetEvent(false);

            public TestObserver(Queue<Offer> queue) { _updatedOffers = queue; }

            public void OnNext(Offer value)
            {
                lock (_updatedOffers)
                {
                    _updatedOffers.Enqueue(value);
                }

                _notified.Set();
            }

            public void WaitForNotification() { _notified.WaitOne(); _notified.Reset(); }

            public void OnCompleted() { throw new NotImplementedException(); }

            public void OnError(Exception error) { throw new NotImplementedException(); }
        }

        [TestInitialize]
        public void InitializeTestDatabase()
        {
            var testDatabase = new TestDatabase.Database();

            {
                var clients = new Dictionary<int, Data.Client>();

                clients.Add(1, new Data.Client { name = "1name", surname = "1surname", password = "1password" });

                testDatabase.SetClientRepo(clients);
            }
            {
                var inventory = new Dictionary<int, Data.Inventory>();

                inventory.Add(1, new Data.Inventory { name = "1name", description = "1description", count = 1, size = new Data.InventorySize(2, 3, 4) });
                inventory.Add(2, new Data.Inventory { name = "2name", description = "2description", count = 2, size = new Data.InventorySize(9, 9, 9) });

                testDatabase.SetInventoryRepo(inventory);
            }
            {
                var offers = new Dictionary<int, Data.Offer>();

                offers.Add(1, new Data.Offer { inventoryId = 1, sellPrice = 10 });
                offers.Add(2, new Data.Offer { inventoryId = 2, sellPrice = 20 });

                testDatabase.SetOfferRepo(offers);
            }

            _logic = new Impl.Logic(testDatabase, 10);
            _clientLogic = _logic.GetClientLogic(1, "1password");
        }
    }
}
