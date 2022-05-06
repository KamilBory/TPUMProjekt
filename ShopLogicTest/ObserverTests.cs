using Microsoft.VisualStudio.TestTools.UnitTesting;

using Data = ShopData.Interface;
using Impl = ShopLogic.Basic;

using ShopLogic.Interface;

using System;
using System.Reflection;
using System.Collections.Generic;

namespace ShopLogicTest
{
    [TestClass]
    public class ObserverTests
    {
        private ILogic _logic;
        private IClientLogic _clientLogic;

        private void UpdateOffer(IClientLogic clientLogic, IOffer offer)
        {
            Assert.IsNotNull(clientLogic);

            var parameters = new object[1];
            parameters[0] = offer;

            typeof(Impl.ClientLogic).GetMethod("UpdateOffer", BindingFlags.Public | BindingFlags.Instance).Invoke(clientLogic, parameters);
        }

        [TestMethod]
        public void OfferSubscribe_Works()
        {
            var observer = new TestOfferObserver_Simple();

            var unsubscriber = _clientLogic.SubscribeForOfferUpdate(observer);

            Assert.IsFalse(observer.calledOnNext);
            Assert.IsFalse(observer.calledOnCompleted);
            Assert.IsFalse(observer.calledOnError);

            Assert.IsNull(observer.updatedOffer);

            var newOffer = _logic.CreateOffer();
            newOffer.id = 68;

            UpdateOffer(_clientLogic, newOffer);

            Assert.IsTrue(observer.calledOnNext);
            Assert.IsFalse(observer.calledOnCompleted);
            Assert.IsFalse(observer.calledOnError);

            Assert.IsNotNull(observer.updatedOffer);
            Assert.AreEqual(observer.updatedOffer.id, newOffer.id);

            unsubscriber.Dispose();

            newOffer = _logic.CreateOffer();
            newOffer.id = 419;

            observer.calledOnNext = false;
            observer.updatedOffer = null;

            UpdateOffer(_clientLogic, newOffer);

            Assert.IsFalse(observer.calledOnNext);
            Assert.IsFalse(observer.calledOnCompleted);
            Assert.IsFalse(observer.calledOnError);

            Assert.IsNull(observer.updatedOffer);
        }

        [TestMethod]
        public void OfferSubscribe_WorksWithMultipleOffers()
        {
            var observer = new TestOfferObserver_Better();

            var unsubscriber = _clientLogic.SubscribeForOfferUpdate(observer);

            var newOffer = _logic.CreateOffer();
            newOffer.id = 67;
            UpdateOffer(_clientLogic, newOffer);

            newOffer = _logic.CreateOffer();
            newOffer.id = 68;
            UpdateOffer(_clientLogic, newOffer);

            int count = 0;

            while (true)
            {
                var offer = observer.PullNext();

                if (offer == null) { break; }

                Assert.AreEqual(offer.id, 67 + count);
                ++count;
            }

            Assert.AreEqual(count, 2);

            unsubscriber.Dispose();

            newOffer.id = 419;

            UpdateOffer(_clientLogic, newOffer);

            while (true)
            {
                var offer = observer.PullNext();

                if (offer == null) { break; }

                Assert.Fail();
            }
        }

        [TestMethod]
        public void OfferSubscribe_ProbablyTheRightWay()
        {
            Queue<IOffer> queue = new Queue<IOffer>();

            var observer = new TestOfferObserver_ProbablyTheRightWay(queue);

            var unsubscriber = _clientLogic.SubscribeForOfferUpdate(observer);

            var newOffer = _logic.CreateOffer();
            newOffer.id = 67;
            UpdateOffer(_clientLogic, newOffer);

            newOffer = _logic.CreateOffer();
            newOffer.id = 68;
            UpdateOffer(_clientLogic, newOffer);

            Assert.AreEqual(queue.Count, 2);
            Assert.AreEqual(queue.Dequeue().id, 67);
            Assert.AreEqual(queue.Dequeue().id, 68);

            unsubscriber.Dispose();

            UpdateOffer(_clientLogic, newOffer);

            Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
        }

        public class TestOfferObserver_Simple : IObserver<IOffer>
        {
            public bool calledOnCompleted = false;
            public bool calledOnError = false;
            public bool calledOnNext = false;

            public IOffer updatedOffer;

            public void OnCompleted() { calledOnCompleted = true; }
            public void OnError(Exception error) { calledOnError = true; }

            // This is only a test implementation. You're not really supposed to
            // pool observer for value (updatedOffer). Instead observer's OnNext
            // method should execute code that updates appropriate values in the
            // GUI immediately. Thus you should provide observer implementation
            // with the proper objects and data that will let it do it properly.
            // Also, you should keep in mind that it gets called asynchronously,
            // so proper synchronisation mechanisms are needed (Mutex at least).
            public void OnNext(IOffer value)
            {
                calledOnNext = true;
                updatedOffer = value;
            }
        }

        public class TestOfferObserver_Better : IObserver<IOffer>
        {
            public Queue<IOffer> updatedOffers = new Queue<IOffer>();

            // This is still pooling. Still suboptimal, but reasonable if you
            // wrote an event loop in a program that is running constantly in
            // the background anyway. 
            public IOffer PullNext()
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
            public void OnNext(IOffer value)
            {
                lock (updatedOffers)
                {
                    updatedOffers.Enqueue(value);
                }
            }

            public void OnCompleted() { throw new NotImplementedException(); }
            public void OnError(Exception error) { throw new NotImplementedException(); }
        }

        public class TestOfferObserver_ProbablyTheRightWay : IObserver<IOffer>
        {
            private Queue<IOffer> updatedOffers;

            public TestOfferObserver_ProbablyTheRightWay(Queue<IOffer> queue) { updatedOffers = queue; }

            // You don't pull the changes explicitly. Instead the
            // observer acts on objects that you provided to it.
            public void OnNext(IOffer value)
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
                var clients = new Dictionary<int, Data.IClient>();

                clients.Add(1, testDatabase.CreateClient("1name", "1surname", "1password"));

                testDatabase.SetClientRepo(clients);
            }
            {
                var offers = new Dictionary<int, Data.IOffer>();

                offers.Add(1, testDatabase.CreateOffer(10, "1name", "1description", 1));
                offers.Add(2, testDatabase.CreateOffer(20, "2name", "2description", 2));

                testDatabase.SetOfferRepo(offers);
            }

            _logic = new Impl.Logic(testDatabase);
            _clientLogic = _logic.GetClientLogic(1, "1password");
        }
    }
}
