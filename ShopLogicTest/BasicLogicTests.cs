using Microsoft.VisualStudio.TestTools.UnitTesting;

using Data = ShopData.Interface;
using Impl = ShopLogic.Basic;

using ShopLogic.Interface;
using System;
using System.Collections.Generic;

namespace ShopLogicTest
{
    [TestClass]
    public class BasicLogicTests
    {
        private ILogic _logic;
        private IClientLogic _clientLogic;

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
        public void Client_InvalidId()
        {
            Assert.ThrowsException<Exception>(delegate { _logic.GetClientLogic(3, "testPassword"); }, "Invalid client id");
        }

        [TestMethod]
        public void Client_InvalidPassword()
        {
            var clientId = _logic.RegisterClient("name", "surname", "correctPassword");
            Assert.ThrowsException<Exception>(delegate { _logic.GetClientLogic(clientId, "wrongPassword"); }, "Invalid password");
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

        [TestMethod]
        public void GetOfferById_InvalidId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.GetOfferById(3); }, "Invalid offer id");
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
        public void DeleteShoppingCart_InvalidId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.DeleteShoppingCart(3); }, "Invalid shop cart id");
        }

        [TestMethod]
        public void AddOfferToShoppingCart_HappyPath()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 1);
        }

        [TestMethod]
        public void AddOfferToShoppingCart_InvalidShopCartId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.AddOfferToShoppingCart(3, 1, 1); }, "Invalid shop cart id");
        }

        [TestMethod]
        public void AddOfferToShoppingCart_InvalidOfferId()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            Assert.ThrowsException<Exception>(delegate { _clientLogic.AddOfferToShoppingCart(shopCartId, 3, 1); }, "Invalid offer id");
        }

        [TestMethod]
        public void AddOfferToShoppingCart_InvalidCount()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            Assert.ThrowsException<Exception>(delegate { _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 0); }, "Invalid offer id");
        }

        [TestMethod]
        public void AddOfferToShoppingCart_CountHigherThanAvailable()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();

            Assert.ThrowsException<Exception>(delegate { _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 2); }, "Tried to add more than available");
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
        public void DeleteOfferFromShoppingCart_InvalidShopCartId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.DeleteOfferFromShoppingCart(3, 1, 1); }, "Invalid shop cart id");
        }

        [TestMethod]
        public void DeleteOfferFromShoppingCart_InvalidOfferId()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 1);

            Assert.ThrowsException<Exception>(delegate { _clientLogic.DeleteOfferFromShoppingCart(shopCartId, 3, 1); }, "Invalid offer id");
        }

        [TestMethod]
        public void DeleteOfferFromShoppingCart_InvalidCount()
        {
            var shopCartId = _clientLogic.CreateShoppingCart();
            _clientLogic.AddOfferToShoppingCart(shopCartId, 1, 1);

            Assert.ThrowsException<Exception>(delegate { _clientLogic.DeleteOfferFromShoppingCart(shopCartId, 1, -1); }, "Invalid count parameter");
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
        public void CreateOrderFromShoppingCart_InvalidShopCartId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.CreateOrderFromShoppingCart(3); }, "Invalid shop cart id");
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
        public void GetOrderById_InvalidId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.GetOrderById(2); }, "Invalid order id");
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
        public void GetOfferChoiceById_InvalidId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.GetOfferChoiceById(69); }, "Invalid offer choice id");
        }

        [TestMethod]
        public void GetShopCartById_HappyPath()
        {
            _clientLogic.GetShopCartById(1);
        }

        [TestMethod]
        public void GetShopCartById_InvalidId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.GetShopCartById(420); }, "Invalid shop cart id");
        }

        [TestInitialize]
        public void InitializeTestDatabase()
        {
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

            _logic = new Impl.Logic(testDatabase);
            _clientLogic = _logic.GetClientLogic(1, "1password");
        }
    }
}
