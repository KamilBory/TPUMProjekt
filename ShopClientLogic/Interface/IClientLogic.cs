using System;

namespace ShopClientLogic.Interface
{
    public interface IClientLogic
    {
        // interactive interface

        IOffer[] GetAllOffers();

        IOffer GetOfferById(int offerId);

        IShopCart[] GetAllShopCarts();

        int CreateShoppingCart();

        bool DeleteShoppingCart(int shopCartId);

        bool AddOfferToShoppingCart(int shopCartId, int offerId, int count);

        bool DeleteOfferFromShoppingCart(int shopCartId, int offerId, int count);

        IOrder CreateOrderFromShoppingCart(int shopCartId);

        IOrder[] GetAllOrders();

        IOrder GetOrderById(int orderId);

        IOfferChoice GetOfferChoiceById(int offerChoiceId);

        IShopCart GetShopCartById(int shopCartId);

        IClient Get();

        bool Update(IClient client);

        // reactive interface

        IDisposable SubscribeForOfferUpdate(IObserver<IOffer> observer);
    }
}
