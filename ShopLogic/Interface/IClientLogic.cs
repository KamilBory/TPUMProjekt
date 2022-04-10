namespace ShopLogic.Interface
{
    public interface IClientLogic
    {
        // TODO all functions throw exceptions on failure (to be implemented)

        Offer[] GetAllOffers();

        Offer GetOfferById(int offerId);

        DeliveryOption[] GetDeliveryOptionsForOffer(int offerId);

        DeliveryOption[] GetDeliveryOptionsForShopCart(int offerId);

        DeliveryOption GetDeliveryOptionById(int deliveryOptionId);

        ShopCart[] GetAllShopCarts();

        int CreateShoppingCart();

        void DeleteShoppingCart(int shopCartId);

        void AddOfferToShoppingCart(int shopCartId, int offerId, int count);

        void DeleteOfferFromShoppingCart(int shopCartId, int offerId, int count);

        Order CreateOrderFromShoppingCart(int shopCartId, int deliveryOptionId);

        Order[] GetAllOrders();

        Order GetOrderById(int orderId);

        ShopCart.OfferChoice GetOfferChoiceById(int offerChoiceId);

        ShopCart GetShopCartById(int shopCartId);

        Client Get();

        void Update(Client client);
    }
}
