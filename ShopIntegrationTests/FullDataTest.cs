using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SSP = ShopServerPresentation;
using SSC = ShopClientData;
using SCL = ShopClientLogic;
using Data = ShopData.Interface;

namespace ShopIntegrationTests
{
    [TestClass]
    public class FullDataTest
    {
        SCL.Interface.ILogic _logic;
        SCL.Interface.IClientLogic _clientLogic;

        Task serverTask;

        SSP.WebSocketConnection wss;
        SSC.WebSocketConnection wsc;

        static int portNum = 8100;

        [TestMethod]
        public void Client_HappyPath()
        {
            string testName = "testName";
            string testSurname = "testSurname";
            string testPassword = "testPassword";

            var newClientId = _logic.RegisterClient(testName, testSurname, testPassword);
            var clientLogic = _logic.GetClientLogic(newClientId, testPassword);

            {
                var clientData = clientLogic.Get();

                Assert.AreEqual(newClientId, clientData.id);
                Assert.AreEqual(testName, clientData.name);
                Assert.AreEqual(testSurname, clientData.surname);
            }

            {
                string newName = "newName";
                string newSurname = "newSurname";

                clientLogic.Update(_logic.CreateClient(newName, newSurname));

                var clientData = clientLogic.Get();

                Assert.AreEqual(newClientId, clientData.id);
                Assert.AreEqual(newName, clientData.name);
                Assert.AreEqual(newSurname, clientData.surname);
            }
        }

        [TestMethod]
        public void GetAllOffers_HappyPath()
        {
            var offers = _clientLogic.GetAllOffers();

            Assert.AreEqual(2, offers.Length);

            Assert.AreEqual(1, offers[0].id);
            Assert.AreEqual("1name", offers[0].name);
            Assert.AreEqual("1description", offers[0].description);
            Assert.AreEqual(1, offers[0].count);
            Assert.AreEqual(10, offers[0].sellPrice);

            Assert.AreEqual(2, offers[1].id);
            Assert.AreEqual("2name", offers[1].name);
            Assert.AreEqual("2description", offers[1].description);
            Assert.AreEqual(2, offers[1].count);
            Assert.AreEqual(20, offers[1].sellPrice);
        }

        [TestMethod]
        public void GetOfferById_HappyPath()
        {
            var offer = _clientLogic.GetOfferById(1);

            Assert.AreEqual(1, offer.id);
            Assert.AreEqual("1name", offer.name);
            Assert.AreEqual("1description", offer.description);
            Assert.AreEqual(1, offer.count);
            Assert.AreEqual(10, offer.sellPrice);
        }

        [TestInitialize]
        public void setup()
        {
            // server
            serverTask = Task.Run(async () => { await SSP.Server.Run(portNum, (wsc) => { SSP.Program.ConnectionHandler(wsc); wss = wsc; }); });

            // connection
            _logic = new SCL.Basic.Logic(portNum);
            wsc = SSC.WebSocketClient.CurrentConnection;

            // inject logic/database
            var testDatabase = new TestDatabase.Database();

            {
                var clients = new Dictionary<int, Data.IClient>();

                testDatabase.CreateClient("1name", "1surname", "1password");

                clients.Add(1, testDatabase.CreateClient("1name", "1surname", "1password"));
                clients.Add(2, testDatabase.CreateClient("2name", "2surname", "2password"));

                testDatabase.SetClientRepo(clients);
            }
            {
                var offers = new Dictionary<int, Data.IOffer>();

                testDatabase.CreateOffer(10, "1name", "1description", 1);

                offers.Add(1, testDatabase.CreateOffer(10, "1name", "1description", 1));
                offers.Add(2, testDatabase.CreateOffer(20, "2name", "2description", 2));

                testDatabase.SetOfferRepo(offers);
            }
            {
                var shopCarts = new Dictionary<int, Data.IShopCart>();

                shopCarts.Add(1, testDatabase.CreateShopCart(1, new HashSet<int>()));
                shopCarts.Add(2, testDatabase.CreateShopCart(2, new HashSet<int>()));

                testDatabase.SetShopCartRepo(shopCarts);
            }

            SSP.Handlers.ReplaceLogic(new ShopLogic.Basic.Logic(testDatabase, 10));
            _clientLogic = _logic.GetClientLogic(1, "1password");
        }

        [TestMethod]
        public void GetAllShopCarts_HappyPath()
        {
            var shopCarts = _clientLogic.GetAllShopCarts();

            Assert.AreEqual(1, shopCarts.Length);
        }

        [TestMethod]
        public void CreateShoppingCart_HappyPath()
        {
            _clientLogic.CreateShoppingCart();
        }

        [TestMethod]
        public void DeleteShoppingCart_HappyPath()
        {
            _clientLogic.DeleteShoppingCart(1);
        }

        [TestMethod]
        public void AddOfferToShoppingCart_HappyPath()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 1);
        }

        [TestMethod]
        public void DeleteOfferFromShoppingCart_HappyPath()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 1);

            _clientLogic.DeleteOfferFromShoppingCart(shopCartId, 1, 1);
        }

        [TestMethod]
        public void DeleteOfferFromShoppingCart_HappyPathDeleteAll()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 1);

            _clientLogic.DeleteOfferFromShoppingCart(shopCartId, 1, 0);
        }

        [TestMethod]
        public void CreateOrderFromShoppingCart_HappyPath()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 1);
            _clientLogic.AddOfferToShoppingCart(shopCartId, 2, 2);

            var order = _clientLogic.CreateOrderFromShoppingCart(shopCartId);

            Assert.AreEqual(order.offerChoicesIds.Length, 2);
        }

        [TestMethod]
        public void GetAllOrders_HappyPath()
        {
            CreateOrderFromShoppingCart_HappyPath();

            var orders = _clientLogic.GetAllOrders();

            Assert.AreEqual(1, orders.Length);
        }

        [TestMethod]
        public void GetOrderById_HappyPath()
        {
            CreateOrderFromShoppingCart_HappyPath();
            var orderId = _clientLogic.GetAllOrders()[0].id;

            _clientLogic.GetOrderById(orderId);
        }

        [TestMethod]
        public void GetOfferChoiceById_HappyPath()
        {
            AddOfferToShoppingCart_HappyPath();

            var shopCarts = _clientLogic.GetAllShopCarts();
            var i = shopCarts.Length - 1;
            var shopCart = shopCarts[i];

            var offerChoice = _clientLogic.GetOfferChoiceById(shopCart.offerChoiceIds[0]);
        }

        [TestMethod]
        public void GetShopCartById_HappyPath()
        {
            _clientLogic.GetShopCartById(1);
        }

        public void PokeServer()
        {
            try
            {
                Task.Run(async () =>
                {
                    await SSC.WebSocketClient.Connect(new Uri($"ws://localhost:{portNum}/"), null, null);
                }).Wait();
            }
            catch
            {
            }
        }

        [TestMethod]
        public void ReactiveWorkflow_SingleObserver()
        {
            Queue<SCL.Interface.IOffer> queue = new Queue<SCL.Interface.IOffer>();

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

        //[TestMethod]
        //public void ReactiveWorkflow_MultipleObservers()
        //{
        //    Queue<SCL.Interface.IOffer> queue1 = new Queue<SCL.Interface.IOffer>();
        //    Queue<SCL.Interface.IOffer> queue2 = new Queue<SCL.Interface.IOffer>();

        //    var observer1 = new TestObserver(queue1);
        //    var observer2 = new TestObserver(queue2);

        //    var unsubscriber1 = _clientLogic.SubscribeForOfferUpdate(observer1);
        //    var unsubscriber2 = _clientLogic.SubscribeForOfferUpdate(observer2);

        //    observer1.WaitForNotification();
        //    observer2.WaitForNotification();

        //    int offerId;
        //    int price;

        //    lock (queue1)
        //    {
        //        var offer = queue1.Dequeue();
        //        offerId = offer.id;
        //        price = offer.sellPrice;
        //    }

        //    lock (queue2)
        //    {
        //        var offer = queue2.Dequeue();
        //        Assert.AreEqual(offer.id, offerId);
        //        Assert.AreEqual(offer.sellPrice, price);
        //    }

        //    observer1.WaitForNotification();
        //    observer2.WaitForNotification();

        //    unsubscriber1.Dispose();
        //    unsubscriber2.Dispose();

        //    _logic.Shutdown();
        //}

        public class TestObserver : IObserver<SCL.Interface.IOffer>
        {
            private Queue<SCL.Interface.IOffer> _updatedOffers;
            private AutoResetEvent _notified = new AutoResetEvent(false);

            public TestObserver(Queue<SCL.Interface.IOffer> queue) { _updatedOffers = queue; }

            public void OnNext(SCL.Interface.IOffer value)
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

        [TestCleanup]
        public void Cleanup()
        {
            wss?.Disconnect().Wait();
            if (wsc != null && wsc.IsConnected) { wsc.Disconnect().Wait(); }

            SSP.Server.cts.Cancel();
            PokeServer();
            serverTask.Wait();

            portNum += 2;
        }
    }
}
