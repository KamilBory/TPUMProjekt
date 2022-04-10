using Data = ShopData.Interface;
using ShopLogic.Interface;
using System;
using System.Collections.Generic;
using System.Collections;

namespace ShopLogic.Basic
{
    public class ClientLogic : IClientLogic
    {
        readonly int _currentClientId;
        Data.IDatabase _database;

        public ClientLogic(int clientId, string password, Data.IDatabase database)
        {
            _currentClientId = clientId;
            _database = database;

            var clientRepo = _database.GetClientRepo();

            var dbClient = clientRepo.Get(_currentClientId) ?? throw new Exception("Invalid client id");

            if (dbClient.password != password) { throw new Exception("Invalid password"); }
        }

        public Offer[] GetAllOffers()
        {
            var offerRepo = _database.GetOfferRepo();
            var inventoryRepo = _database.GetInventoryRepo();

            var dbOffers = offerRepo.List();
            var offers = new Offer[dbOffers.Length];

            for (int i = 0; i < dbOffers.Length; ++i)
            {
                var dbInventory = inventoryRepo.Get(dbOffers[i].Value.inventoryId) ?? throw new Exception("Invalid inventory id");
                offers[i] = Utilities.Convert(dbOffers[i].Value, dbInventory, dbOffers[i].Key);
            }

            return offers;
        }

        public Offer GetOfferById(int offerId)
        {
            var offerRepo = _database.GetOfferRepo();
            var inventoryRepo = _database.GetInventoryRepo();

            var dbOffer = offerRepo.Get(offerId) ?? throw new Exception("Invalid offer id");
            var dbInventory = inventoryRepo.Get(dbOffer.inventoryId) ?? throw new Exception("Invalid inventory id");

            return Utilities.Convert(dbOffer, dbInventory, offerId);
        }

        public DeliveryOption[] GetDeliveryOptionsForOffer(int offerId)
        {
            var offerRepo = _database.GetOfferRepo();
            var inventoryRepo = _database.GetInventoryRepo();
            var deliveryOptionRepo = _database.GetDeliveryOptionRepo();

            var deliveryOptions = new List<DeliveryOption>();

            var dbDeliveryOptions = deliveryOptionRepo.List();
            var dbOffer = offerRepo.Get(offerId) ?? throw new Exception("Invalid offer id");
            var dbInventory = inventoryRepo.Get(dbOffer.inventoryId) ?? throw new Exception("Invalid inventory id");

            foreach (var dbDeliveryOption in dbDeliveryOptions)
            {
                if (Utilities.DeliveryOptionAvailableForInventory(dbDeliveryOption.Value, dbInventory))
                {
                    deliveryOptions.Add(Utilities.Convert(dbDeliveryOption.Value, dbDeliveryOption.Key));
                }
            }

            return deliveryOptions.ToArray();
        }

        public DeliveryOption[] GetDeliveryOptionsForShopCart(int shopCartId)
        {
            var shopCartRepo = _database.GetShopCartRepo();
            var offerRepo = _database.GetOfferRepo();
            var offerChoiceRepo = _database.GetOfferChoiceRepo();
            var inventoryRepo = _database.GetInventoryRepo();
            var deliveryOptionRepo = _database.GetDeliveryOptionRepo();

            var dbShopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");

            var deliveryOptions = new List<DeliveryOption>();
            var dbDeliveryOptions = deliveryOptionRepo.List();

            foreach (var dbDeliveryOption in dbDeliveryOptions)
            {
                bool deliveryOptionAvailable = true;

                foreach (var offerChoiceId in dbShopCart.offerChoiceIds)
                {
                    var dbOfferChoice = offerChoiceRepo.Get(offerChoiceId) ?? throw new Exception("Invalid offer choice id");
                    var dbOffer = offerRepo.Get(dbOfferChoice.offerId) ?? throw new Exception("Invalid offer id");
                    var dbInventory = inventoryRepo.Get(dbOffer.inventoryId) ?? throw new Exception("Invalid inventory id");

                    deliveryOptionAvailable &= Utilities.DeliveryOptionAvailableForInventory(dbDeliveryOption.Value, dbInventory);

                    if (!deliveryOptionAvailable) { break; }
                }

                if (deliveryOptionAvailable)
                {
                    deliveryOptions.Add(Utilities.Convert(dbDeliveryOption.Value, dbDeliveryOption.Key));
                }
            }

            return deliveryOptions.ToArray();
        }

        public int CreateShoppingCart()
        {
            var shopCartRepo = _database.GetShopCartRepo();
            var newDbShopCart = new Data.ShopCart { clientId = _currentClientId, offerChoiceIds = new HashSet<int>() };
            return shopCartRepo.Create(newDbShopCart);
        }

        public void DeleteShoppingCart(int shopCartId)
        {
            var shopCartRepo = _database.GetShopCartRepo();
            if (!shopCartRepo.Delete(shopCartId)) { throw new Exception("Failed to delete shop cart"); }
        }

        public void AddOfferToShoppingCart(int shopCartId, int offerId, int count)
        {
            if (count <= 0) throw new Exception("Invalid count parameter");

            var offerRepo = _database.GetOfferRepo();
            var offerChoiceRepo = _database.GetOfferChoiceRepo();
            var shopCartRepo = _database.GetShopCartRepo();
            var inventoryRepo = _database.GetInventoryRepo();

            var dbOffer = offerRepo.Get(offerId) ?? throw new Exception("Invalid offer id");
            var dbShopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");
            var dbInventory = inventoryRepo.Get(dbOffer.inventoryId) ?? throw new Exception("Invalid inventory id");

            foreach (var dbOfferChoiceId in dbShopCart.offerChoiceIds)
            {
                var dbOfferChoice = offerChoiceRepo.Get(dbOfferChoiceId) ?? throw new Exception("Invalid offer choice id");

                if (dbOfferChoice.offerId != offerId) { continue; }

                dbOfferChoice.count += count;

                if (dbOfferChoice.count > dbInventory.count) { throw new Exception("Tried to add more than available"); }

                if (!offerChoiceRepo.Update(dbOfferChoiceId, dbOfferChoice)) { throw new Exception("Failed to update offer choice"); }

                return;
            }

            var newDbOfferChoice = new Data.OfferChoice
            {
                offerId = offerId,
                count = count
            };

            if (newDbOfferChoice.count > dbInventory.count) { throw new Exception("Tried to add more than available"); }

            var newDbOfferChoiceId = offerChoiceRepo.Create(newDbOfferChoice);

            dbShopCart.offerChoiceIds.Add(newDbOfferChoiceId);

            if (!shopCartRepo.Update(shopCartId, dbShopCart)) { throw new Exception("Failed to update shop cart"); }
        }

        public void DeleteOfferFromShoppingCart(int shopCartId, int offerId, int count)
        {
            if (count < 0) throw new Exception("Invalid count parameter");

            var shopCartRepo = _database.GetShopCartRepo();
            var offerChoiceRepo = _database.GetOfferChoiceRepo();

            var dbShopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");

            foreach (var dbOfferChoiceId in dbShopCart.offerChoiceIds)
            {
                var dbOfferChoice = offerChoiceRepo.Get(dbOfferChoiceId) ?? throw new Exception("Invalid offer choice id");

                if (dbOfferChoice.offerId != offerId) { continue; }

                dbOfferChoice.count -= count;

                if (dbOfferChoice.count <= 0 || count == 0)
                {
                    if (!offerChoiceRepo.Delete(dbOfferChoiceId)) { throw new Exception("Failed to delete offer choice"); }
                    dbShopCart.offerChoiceIds.Remove(dbOfferChoiceId);
                    if (!shopCartRepo.Update(shopCartId, dbShopCart)) { throw new Exception("Failed to update shop cart"); }
                }
                else
                {
                    if (!offerChoiceRepo.Update(dbOfferChoiceId, dbOfferChoice)) { throw new Exception("Failed to update offer choice"); }
                }

                return;
            }

            throw new Exception("Invalid offer id");
        }

        public Order CreateOrderFromShoppingCart(int shopCartId, int deliveryOptionId)
        {
            var shopCartRepo = _database.GetShopCartRepo();
            var orderRepo = _database.GetOrderRepo();

            var dbShopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");

            var newDbOrder = new Data.Order
            {
                clientId = _currentClientId,
                offerChoiceIds = dbShopCart.offerChoiceIds,
                deliveryOptionId = deliveryOptionId,
                creationTime = DateTime.UtcNow,
                state = Data.OrderState.WAITING
            };

            var offerChoicesIds = new int[dbShopCart.offerChoiceIds.Count];
            dbShopCart.offerChoiceIds.CopyTo(offerChoicesIds);

            var newDbOrderId = orderRepo.Create(newDbOrder);

            shopCartRepo.Delete(shopCartId);

            return new Order
            {
                id = newDbOrderId,
                offerChoicesIds = offerChoicesIds,
                deliveryOptionId = deliveryOptionId,
                state = Utilities.Convert(newDbOrder.state)
            };
        }

        public Order[] GetAllOrders()
        {
            var orderRepo = _database.GetOrderRepo();

            var dbOrders = orderRepo.List();
            Utilities.Filter(ref dbOrders, _currentClientId);

            return Utilities.Convert(dbOrders);
        }

        public Order GetOrderById(int orderId)
        {
            var orderRepo = _database.GetOrderRepo();
            var dbOrder = orderRepo.Get(orderId) ?? throw new Exception("Invalid order id");
            return Utilities.Convert(dbOrder, orderId);
        }

        public DeliveryOption GetDeliveryOptionById(int deliveryOptionId)
        {
            var deliveryOptionRepo = _database.GetDeliveryOptionRepo();
            var dbDeliveryOption = deliveryOptionRepo.Get(deliveryOptionId) ?? throw new Exception("Invalid delivery option id");
            return Utilities.Convert(dbDeliveryOption, deliveryOptionId);
        }

        public ShopCart.OfferChoice GetOfferChoiceById(int offerChoiceId)
        {
            var offerChoiceRepo = _database.GetOfferChoiceRepo();
            var dbOfferChoice = offerChoiceRepo.Get(offerChoiceId) ?? throw new Exception("Invalid offer choice id");
            return Utilities.Convert(dbOfferChoice, offerChoiceId);
        }

        public ShopCart GetShopCartById(int shopCartId)
        {
            var shopCartRepo = _database.GetShopCartRepo();
            var shopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");
            return Utilities.Convert(shopCart, shopCartId);
        }

        public Client Get()
        {
            var clientRepo = _database.GetClientRepo();
            var dbClient = clientRepo.Get(_currentClientId) ?? throw new Exception("Invalid client id");
            return Utilities.Convert(dbClient, _currentClientId);
        }

        public void Update(Client client)
        {
            var clientRepo = _database.GetClientRepo();
            if (!clientRepo.Update(_currentClientId, Utilities.Convert(client))) { throw new Exception("Failed to update client"); }
        }
    }
}
