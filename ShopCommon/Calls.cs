using ShopCommon.Data;

namespace ShopCommon.Calls
{
    public enum Type
    {
        NULL,
        GET_CLIENT,
        UPDATE_CLIENT,
        REGISTER_CLIENT,
        GET_ALL_OFFERS,
        GET_OFFER_BY_ID,
        GET_OFFER_CHOICE_BY_ID,
        GET_ALL_SHOPCARTS,
        CREATE_SHOPCART,
        DELETE_SHOPCART,
        ADD_OFFER_TO_SHOPCART,
        DELETE_OFFER_FROM_SHOPCART,
        CREATE_ORDER_FROM_SHOPCART,
        GET_ALL_ORDERS,
        GET_ORDER_BY_ID,
        GET_SHOPCART_BY_ID,
        SUBSCRIBE_FOR_OFFER_UPDATE,
        OFFER_OBSERVER,
    };

    public interface IMessage
    {
        public Type type { get; }
    }

    public class MessageBase : IMessage
    {
        public Type type { get; set; }
    }

    public class RegisterClientRequest : IMessage
    {
        public Type type { get { return Type.REGISTER_CLIENT; } }
        
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
    }

    public class RegisterClientResponse : IMessage
    {
        public Type type { get { return Type.REGISTER_CLIENT; } }

        public int id { get; set; }
    }

    public class GetClientRequest : IMessage
    {
        public Type type { get { return Type.GET_CLIENT; } }

        public int id { get; set; }
        public string password { get; set; }
    }

    public class GetClientResponse : IMessage
    {
        public Type type { get { return Type.GET_CLIENT; } }

        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
    }

    public class UpdateClientRequest : IMessage
    {
        public Type type { get { return Type.UPDATE_CLIENT; } }

        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
    }

    public class UpdateClientResponse : IMessage
    {
        public Type type { get { return Type.UPDATE_CLIENT; } }

        public bool success { get; set; }
    }

    public class GetAllOffersRequest : IMessage
    {
        public Type type { get { return Type.GET_ALL_OFFERS; } }

        public int id { get; set; }
        public string password { get; set; }
    }

    public class GetAllOffersResponse : IMessage
    {
        public Type type { get { return Type.GET_ALL_OFFERS; } }

        public bool success { get; set; }
        public Offer[] offers { get; set; }
    }

    public class GetAllOrdersRequest : IMessage
    {
        public Type type { get { return Type.GET_ALL_ORDERS; } }

        public int id { get; set; }
        public string password { get; set; }
    }

    public class GetAllOrdersResponse : IMessage
    {
        public Type type { get { return Type.GET_ALL_ORDERS; } }

        public bool success { get; set; }
        public Order[] orders { get; set; }
    }

    public class GetOfferByIdRequest : IMessage
    {
        public Type type { get { return Type.GET_OFFER_BY_ID; } }

        public int id { get; set; }
        public string password { get; set; }
        public int offerId { get; set; }
    }

    public class GetOfferByIdResponse : IMessage
    {
        public Type type { get { return Type.GET_OFFER_BY_ID; } }

        public bool success { get; set; }
        public Offer offer { get; set; }
    }

    public class GetOrderByIdRequest : IMessage
    {
        public Type type { get { return Type.GET_ORDER_BY_ID; } }

        public int id { get; set; }
        public string password { get; set; }
        public int orderId { get; set; }
    }

    public class GetOrderByIdResponse : IMessage
    {
        public Type type { get { return Type.GET_ORDER_BY_ID; } }

        public bool success { get; set; }
        public Order order { get; set; }
    }

    public class GetShopCartByIdRequest : IMessage
    {
        public Type type { get { return Type.GET_SHOPCART_BY_ID; } }

        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }
    }

    public class GetShopCartByIdResponse : IMessage
    {
        public Type type { get { return Type.GET_SHOPCART_BY_ID; } }

        public bool success { get; set; }
        public ShopCart shopCart { get; set; }
    }

    public class GetOfferChoiceByIdRequest : IMessage
    {
        public Type type { get { return Type.GET_OFFER_CHOICE_BY_ID; } }

        public int id { get; set; }
        public string password { get; set; }
        public int choiceId { get; set; }
    }

    public class GetOfferChoiceByIdResponse : IMessage
    {
        public Type type { get { return Type.GET_OFFER_CHOICE_BY_ID; } }

        public bool success { get; set; }
        public OfferChoice offerChoice { get; set; }
    }

    public class GetAllShopCartsRequest : IMessage
    {
        public Type type { get { return Type.GET_ALL_SHOPCARTS; } }

        public int id { get; set; }
        public string password { get; set; }
    }

    public class GetAllShopCartsResponse : IMessage
    {
        public Type type { get { return Type.GET_ALL_SHOPCARTS; } }

        public bool success { get; set; }
        public ShopCart[] shopCarts { get; set; }
    }

    public class CreateShoppingCartRequest : IMessage
    {
        public Type type { get { return Type.CREATE_SHOPCART; } }

        public int id { get; set; }
        public string password { get; set; }
    }

    public class CreateShoppingCartResponse : IMessage
    {
        public Type type { get { return Type.CREATE_SHOPCART; } }

        public bool success { get; set; }
        public int id { get; set; }
    }

    public class DeleteShoppingCartRequest : IMessage
    {
        public Type type { get { return Type.DELETE_SHOPCART; } }

        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }
    }

    public class DeleteShoppingCartResponse : IMessage
    {
        public Type type { get { return Type.DELETE_SHOPCART; } }

        public bool success { get; set; }
    }

    public class AddOfferToShoppingCartRequest : IMessage
    {
        public Type type { get { return Type.ADD_OFFER_TO_SHOPCART; } }

        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }
        public int offerId { get; set; }
        public int count { get; set; }
    }

    public class AddOfferToShoppingCartResponse : IMessage
    {
        public Type type { get { return Type.ADD_OFFER_TO_SHOPCART; } }

        public bool success { get; set; }
    }

    public class DeleteOfferFromShoppingCartRequest : IMessage
    {
        public Type type { get { return Type.DELETE_OFFER_FROM_SHOPCART; } }

        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }
        public int offerId { get; set; }
        public int count { get; set; }
    }

    public class DeleteOfferFromShoppingCartResponse : IMessage
    {
        public Type type { get { return Type.DELETE_OFFER_FROM_SHOPCART; } }

        public bool success { get; set; }
    }

    public class CreateOrderFromShoppingCartRequest : IMessage
    {
        public Type type { get { return Type.CREATE_ORDER_FROM_SHOPCART; } }

        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }
    }

    public class CreateOrderFromShoppingCartResponse : IMessage
    {
        public Type type { get { return Type.CREATE_ORDER_FROM_SHOPCART; } }

        public Order order { get; set; }
    }

    public class OfferUpdateSubscriptionRequest : IMessage
    {
        public Type type { get { return Type.SUBSCRIBE_FOR_OFFER_UPDATE; } }

        public int id { get; set; }
        public string password { get; set; }
    }

    public class OfferUpdateSubscriptionResponse : IMessage
    {
        public Type type { get { return Type.SUBSCRIBE_FOR_OFFER_UPDATE; } }

        public bool success { get; set; }
    }

    public class OfferUpdateNotification : IMessage
    {
        public Type type { get { return Type.OFFER_OBSERVER; } }

        public Offer offer { get; set; }
    }
}
