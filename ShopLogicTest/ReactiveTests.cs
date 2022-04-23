using Microsoft.VisualStudio.TestTools.UnitTesting;

using Data = ShopData.Interface;
using Impl = ShopLogic.Basic;

using ShopLogic.Interface;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace ShopLogicTest
{
    [TestClass]
    public class ReactiveTests
    {
        private ILogic _logic;
        private IClientLogic _clientLogic;

        private void UpdateOffer(IClientLogic clientLogic, Offer offer)
        {
            var parameters = new object[1];
            parameters[0] = offer;

            typeof(Impl.ClientLogic).GetMethod("UpdateOffer", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(clientLogic, parameters);
        }

        [TestMethod]
        public void OfferSubscribe_Works()
        {
            var observer = new TestOfferObserver_Simple();

            var unsubscriber = _clientLogic.SubscribeForOfferUpdate(observer);

            Assert.IsFalse(observer.calledOnNext);
            Assert.IsFalse(observer.calledOnCompleted);
            Assert.IsFalse(observer.calledOnError);

            Assert.IsFalse(observer.updatedOffer.HasValue);

            var newOffer = new Offer();
            newOffer.id = 68;

            UpdateOffer(_clientLogic, newOffer);

            Assert.IsTrue(observer.calledOnNext);
            Assert.IsFalse(observer.calledOnCompleted);
            Assert.IsFalse(observer.calledOnError);

            Assert.IsTrue(observer.updatedOffer.HasValue);
            Assert.AreEqual(observer.updatedOffer.Value.id, newOffer.id);

            unsubscriber.Dispose();

            newOffer.id = 419;

            observer.calledOnNext = false;
            observer.updatedOffer = null;

            UpdateOffer(_clientLogic, newOffer);

            Assert.IsFalse(observer.calledOnNext);
            Assert.IsFalse(observer.calledOnCompleted);
            Assert.IsFalse(observer.calledOnError);

            Assert.IsFalse(observer.updatedOffer.HasValue);
        }

        [TestMethod]
        public void OfferSubscribe_WorksWithMultipleOffers()
        {
            var observer = new TestOfferObserver_Better();

            var unsubscriber = _clientLogic.SubscribeForOfferUpdate(observer);

            var newOffer = new Offer();

            newOffer.id = 67;
            UpdateOffer(_clientLogic, newOffer);

            newOffer.id = 68;
            UpdateOffer(_clientLogic, newOffer);

            int count = 0;

            while (true)
            {
                var offer = observer.PullNext();

                if (!offer.HasValue) { break; }

                Assert.AreEqual(offer.Value.id, 67 + count);
                ++count;
            }

            Assert.AreEqual(count, 2);

            unsubscriber.Dispose();

            newOffer.id = 419;

            UpdateOffer(_clientLogic, newOffer);

            while (true)
            {
                var offer = observer.PullNext();

                if (!offer.HasValue) { break; }

                Assert.Fail();
            }
        }

        [TestMethod]
        public void OfferSubscribe_ProbablyTheRightWay()
        {
            Queue<Offer> queue = new Queue<Offer>();

            var observer = new TestOfferObserver_ProbablyTheRightWay(queue);

            var unsubscriber = _clientLogic.SubscribeForOfferUpdate(observer);

            var newOffer = new Offer();

            newOffer.id = 67;
            UpdateOffer(_clientLogic, newOffer);

            newOffer.id = 68;
            UpdateOffer(_clientLogic, newOffer);

            Assert.AreEqual(queue.Count, 2);
            Assert.AreEqual(queue.Dequeue().id, 67);
            Assert.AreEqual(queue.Dequeue().id, 68);

            unsubscriber.Dispose();

            UpdateOffer(_clientLogic, newOffer);

            Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
        }

        public class TestOfferObserver_Simple : IObserver<Offer>
        {
            public bool calledOnCompleted = false;
            public bool calledOnError = false;
            public bool calledOnNext = false;

            public Offer? updatedOffer;

            public void OnCompleted() { calledOnCompleted = true; }
            public void OnError(Exception error) { calledOnError = true; }

            // This is only a test implementation. You're not really supposed to
            // pool observer for value (updatedOffer). Instead observer's OnNext
            // method should execute code that updates appropriate values in the
            // GUI immediately. Thus you should provide observer implementation
            // with the proper objects and data that will let it do it properly.
            // Also, you should keep in mind that it gets called asynchronously,
            // so proper synchronisation mechanisms are needed (Mutex at least).
            public void OnNext(Offer value)
            {
                calledOnNext = true;
                updatedOffer = value;
            }
        }

        public class TestOfferObserver_Better : IObserver<Offer>
        {
            public Queue<Offer> updatedOffers = new Queue<Offer>();

            // This is still pooling. Still suboptimal, but reasonable if you
            // wrote an event loop in a program that is running constantly in
            // the background anyway. 
            public Offer? PullNext()
            {
                lock (updatedOffers)
                {
                    if (updatedOffers.Count > 0)
                    {
                        return updatedOffers.Dequeue();
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            // Minimal synchronisation, but still needs
            // pooling at some point on purpose.
            public void OnNext(Offer value)
            {
                lock (updatedOffers)
                {
                    updatedOffers.Enqueue(value);
                }
            }

            public void OnCompleted() { throw new NotImplementedException(); }
            public void OnError(Exception error) { throw new NotImplementedException(); }
        }

        public class TestOfferObserver_ProbablyTheRightWay : IObserver<Offer>
        {
            private Queue<Offer> updatedOffers;

            public TestOfferObserver_ProbablyTheRightWay(Queue<Offer> queue) { updatedOffers = queue; }

            // You don't pull the changes explicitly. Instead the
            // observer acts on objects that you provided to it.
            public void OnNext(Offer value)
            {
                lock (updatedOffers)
                {
                    updatedOffers.Enqueue(value);
                }
            }

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
                clients.Add(2, new Data.Client { name = "2name", surname = "2surname", password = "2password" });

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
            {
                var deliveryOptions = new Dictionary<int, Data.DeliveryOption>();

                deliveryOptions.Add(1, new Data.DeliveryOption { name = "Courier", maxSize = new Data.InventorySize(5, 5, 5), price = 5 });
                deliveryOptions.Add(2, new Data.DeliveryOption { name = "Self-pickup", maxSize = new Data.InventorySize(500, 500, 500), price = 0 });

                testDatabase.SetDeliveryOptionRepo(deliveryOptions);
            }
            {
                var shopCarts = new Dictionary<int, Data.ShopCart>();

                shopCarts.Add(1, new Data.ShopCart { clientId = 1, offerChoiceIds = new HashSet<int>() });
                shopCarts.Add(2, new Data.ShopCart { clientId = 2, offerChoiceIds = new HashSet<int>() });

                testDatabase.SetShopCartRepo(shopCarts);
            }

            _logic = new Impl.Logic(testDatabase);
            _clientLogic = _logic.GetClientLogic(1, "1password");
        }
    }
}
