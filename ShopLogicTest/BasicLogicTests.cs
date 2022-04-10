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

                clientLogic.Update(new Client { name = newName, surname = newSurname });

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
            Assert.AreEqual(1, offers[0].availableCount);
            Assert.AreEqual(10, offers[0].sellPrice);

            Assert.AreEqual(2, offers[1].id);
            Assert.AreEqual("2name", offers[1].name);
            Assert.AreEqual("2description", offers[1].description);
            Assert.AreEqual(2, offers[1].availableCount);
            Assert.AreEqual(20, offers[1].sellPrice);
        }

        [TestMethod]
        public void GetOfferById_HappyPath()
        {
            var offer = _clientLogic.GetOfferById(1);

            Assert.AreEqual(1, offer.id);
            Assert.AreEqual("1name", offer.name);
            Assert.AreEqual("1description", offer.description);
            Assert.AreEqual(1, offer.availableCount);
            Assert.AreEqual(10, offer.sellPrice);
        }

        [TestMethod]
        public void GetOfferById_InvalidId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.GetOfferById(3); }, "Invalid offer id");
        }

        [TestMethod]
        public void GetDeliveryOptionsForOffer_HappyPath()
        {
            {
                var deliveryOptions = _clientLogic.GetDeliveryOptionsForOffer(1);

                Assert.AreEqual(2, deliveryOptions.Length);

                Assert.AreEqual(1, deliveryOptions[0].id);
                Assert.AreEqual("Courier", deliveryOptions[0].name);
                Assert.AreEqual(5, deliveryOptions[0].price);

                Assert.AreEqual(2, deliveryOptions[1].id);
                Assert.AreEqual("Self-pickup", deliveryOptions[1].name);
                Assert.AreEqual(0, deliveryOptions[1].price);
            }
            {
                var deliveryOptions = _clientLogic.GetDeliveryOptionsForOffer(2);

                Assert.AreEqual(1, deliveryOptions.Length);

                Assert.AreEqual(2, deliveryOptions[0].id);
                Assert.AreEqual("Self-pickup", deliveryOptions[0].name);
                Assert.AreEqual(0, deliveryOptions[0].price);
            }
        }

        [TestMethod]
        public void GetDeliveryOptionById_HappyPath()
        {
            var deliveryOption = _clientLogic.GetDeliveryOptionById(2);

            Assert.AreEqual(2, deliveryOption.id);
            Assert.AreEqual("Self-pickup", deliveryOption.name);
            Assert.AreEqual(0, deliveryOption.price);
        }

        [TestMethod]
        public void GetDeliveryOptionById_InvalidId()
        {
            Assert.ThrowsException<Exception>(delegate { _clientLogic.GetDeliveryOptionById(3); }, "Invalid delivery option id");
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
            Assert.ThrowsException<Exception>(delegate { _clientLogic.DeleteShoppingCart(2); }, "Invalid shop cart id");
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
            Assert.ThrowsException<Exception>(delegate { _clientLogic.AddOfferToShoppingCart(2, 1, 1); }, "Invalid shop cart id");
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
            
            var deliveryOptions = _clientLogic.GetDeliveryOptionsForShopCart(shopCartId);

            Assert.AreEqual(1, deliveryOptions.Length);

            Assert.AreEqual(2, deliveryOptions[0].id);
            Assert.AreEqual("Self-pickup", deliveryOptions[0].name);
            Assert.AreEqual(0, deliveryOptions[0].price);


        }

        [TestMethod]
        public void CreateOrderFromShoppingCart_InvalidShopCartId()
        {
        }

        [TestMethod]
        public void CreateOrderFromShoppingCart_InvalidDeliveryOptionId()
        {
        }

        [TestMethod]
        public void GetAllOrders_HappyPath()
        {
        }

        [TestMethod]
        public void GetOrderById_HappyPath()
        {
        }

        [TestMethod]
        public void GetOrderById_InvalidId()
        {
        }

        [TestMethod]
        public void GetOfferChoiceById_HappyPath()
        {
        }

        [TestMethod]
        public void GetOfferChoiceById_InvalidId()
        {
        }

        [TestMethod]
        public void GetShopCartById_HappyPath()
        {
        }

        [TestMethod]
        public void GetShopCartById_InvalidId()
        {
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

                shopCarts.Add(1, new Data.ShopCart { clientId = 1 });

                testDatabase.SetShopCartRepo(shopCarts);
            }

            _logic = new Impl.Logic(testDatabase);
            _clientLogic = _logic.GetClientLogic(1, "1password");
        }
    }
}
