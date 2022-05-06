using System;
using System.Collections.Generic;

using ShopData.Interface;
using ShopData.Types;

namespace ShopData.MemoryModel
{
    public class Database : IDatabase
    {
        private Repo<IClient> _clientRepo = new Repo<IClient>();
        private Repo<IOffer> _offerRepo = new Repo<IOffer>();
        private Repo<IOfferChoice> _offerChoiceRepo = new Repo<IOfferChoice>();
        private Repo<IOrder> _orderRepo = new Repo<IOrder>();
        private Repo<IShopCart> _shopCartRepo = new Repo<IShopCart>();

        public IRepo<IClient> GetClientRepo() { return _clientRepo; }
        public IRepo<IOffer> GetOfferRepo() { return _offerRepo; }
        public IRepo<IOfferChoice> GetOfferChoiceRepo() { return _offerChoiceRepo; }
        public IRepo<IOrder> GetOrderRepo() { return _orderRepo; }
        public IRepo<IShopCart> GetShopCartRepo() { return _shopCartRepo; }

        public IClient CreateClient() { return new Client(); }
        public IOffer CreateOffer() { return new Offer(); }
        public IOfferChoice CreateOfferChoice() { return new OfferChoice(); }
        public IOrder CreateOrder() { return new Order(); }
        public IShopCart CreateShopCart() { return new ShopCart(); }

        public IClient CreateClient(string name, string surname, string password)
        {
            return new Client { name = name, surname = surname, password = password };
        }

        public IOffer CreateOffer(int sellPrice, string name, string description, int count)
        {
            return new Offer { sellPrice = sellPrice, name = name, description = description, count = count };
        }

        public IOfferChoice CreateOfferChoice(int offerId, int count)
        {
            return new OfferChoice { offerId = offerId, count = count };
        }

        public IOrder CreateOrder(int clientId, HashSet<int> offerChoiceIds, DateTime creationTime, OrderState state)
        {
            return new Order { clientId = clientId, offerChoiceIds = offerChoiceIds, creationTime = creationTime, state = state };
        }

        public IShopCart CreateShopCart(int clientId, HashSet<int> offerChoiceIds)
        {
            return new ShopCart { clientId = clientId, offerChoiceIds = offerChoiceIds };
        }
    }
}
