namespace ShopLogic.Interface
{
    public interface IClientLogic
    {
        // TODO all functions throw exceptions on failure (to be implemented)

        Offer[] GetAllOffers();

        Offer GetOfferById(int offerId);

        DeliveryOption[] GetDeliveryOptionsForOffer(int offerId);

        void AddOfferToShoppingCart(int shopCartId, int offerId, int count);

        Order CreateOrderFromShoppingCart(int shopCartId, int deliveryOptionId);

        Order[] GetAllOrders();

        Order GetOrderById(int orderId);

        DeliveryOption GetDeliveryOptionById(int deliveryOptionId);

        ShopCart.OfferChoice GetOfferChoiceById(int offerChoiceId);

        ShopCart GetShopCartById(int shopCartId);

        Client Get();

        void Update(Client client);
    }
}
