using System;

namespace ShopLogic.Interface
{
    public interface ILogic
    {
        IClientLogic GetClientLogic(int clientId, string password);

        int RegisterClient(string name, string surname, string password);

        void Shutdown();

        IClient CreateClient();
        IOffer CreateOffer();
        IOfferChoice CreateOfferChoice();
        IOrder CreateOrder();
        IShopCart CreateShopCart();

        IClient CreateClient(string name, string surname);
        IOffer CreateOffer(int sellPrice, string name, string description, int count);
        IOfferChoice CreateOfferChoice(int offerId, int count);
        IOrder CreateOrder(int[] offerChoicesIds, DateTime creationTime, OrderState state);
        IShopCart CreateShopCart(int[] offerChoiceIds);
    }
}
