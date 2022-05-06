using System;
using System.Collections.Generic;

namespace ShopData.Interface
{
    public interface IDatabase
    {
        IRepo<IClient> GetClientRepo();
        IRepo<IOffer> GetOfferRepo();
        IRepo<IOfferChoice> GetOfferChoiceRepo();
        IRepo<IOrder> GetOrderRepo();
        IRepo<IShopCart> GetShopCartRepo();

        IClient CreateClient();
        IOffer CreateOffer();
        IOfferChoice CreateOfferChoice();
        IOrder CreateOrder();
        IShopCart CreateShopCart();

        IClient CreateClient(string name, string surname, string password);
        IOffer CreateOffer(int sellPrice, string name, string description, int count);
        IOfferChoice CreateOfferChoice(int offerId, int count);
        IOrder CreateOrder(int clientId, HashSet<int> offerChoiceIds, DateTime creationTime, OrderState state);
        IShopCart CreateShopCart(int clientId, HashSet<int> offerChoiceIds);
    }
}
