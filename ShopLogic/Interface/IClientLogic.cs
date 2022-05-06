using System;

namespace ShopLogic.Interface
{
    public interface IClientLogic
    {
        // interactive interface

        IOffer[] GetAllOffers();

        IOffer GetOfferById(int offerId);

        IShopCart[] GetAllShopCarts();

        int CreateShoppingCart();

        void DeleteShoppingCart(int shopCartId);

        void AddOfferToShoppingCart(int shopCartId, int offerId, int count);

        void DeleteOfferFromShoppingCart(int shopCartId, int offerId, int count);

        IOrder CreateOrderFromShoppingCart(int shopCartId);

        IOrder[] GetAllOrders();

        IOrder GetOrderById(int orderId);

        IOfferChoice GetOfferChoiceById(int offerChoiceId);

        IShopCart GetShopCartById(int shopCartId);

        IClient Get();

        void Update(IClient client);

        // reactive interface

        IDisposable SubscribeForOfferUpdate(IObserver<IOffer> observer);
    }
}
