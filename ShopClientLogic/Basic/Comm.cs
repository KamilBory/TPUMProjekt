using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using ShopClientData;
using ShopClientLogic.Types;

namespace ShopClientLogic.Basic
{
    public enum RequestType
    {
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

    public abstract class AbstractRequest
    {
        public abstract string Serialize();

        static protected string Serialize<T>(T obj) { return JsonSerializer.Serialize(obj); }
    }

    public abstract class AbstractResponse
    {
        public abstract string Serialize();

        static protected string Serialize<T>(T obj) { return JsonSerializer.Serialize(obj); }
    }

    public struct RequestBase
    {
        public RequestType type { get; set; }
    }

    public struct ResponseBase
    {
        public RequestType type { get; set; }
    }

    public struct Request<T> where T : AbstractRequest
    {
        public RequestType type { get; set; }
        public T body { get; set; }
    }

    public struct Response<T> where T : AbstractResponse
    {
        public RequestType type { get; set; }
        public T body { get; set; }
    }

    public class RegisterClientCall : AbstractRequest
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class RegisterClientResponse : AbstractResponse
    {
        public int id { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetClientRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetClientResponse : AbstractResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class UpdateClientRequest : AbstractRequest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class UpdateClientResponse : AbstractResponse
    {
        public bool success { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetAllOffersRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetAllOffersResponse : AbstractResponse
    {
        public bool success { get; set; }
        public Offer[] offers { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetOfferByIdRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public int offerId { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetOfferByIdResponse : AbstractResponse
    {
        public bool success { get; set; }
        public Offer offer { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetOfferChoiceByIdRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public int choiceId { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetOfferChoiceByIdResponse : AbstractResponse
    {
        public bool success { get; set; }
        public OfferChoice offerChoice { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetAllShopCartsRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetAllShopCartsResponse : AbstractResponse
    {
        public bool success { get; set; }
        public ShopCart[] shopCarts { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class CreateShoppingCartRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class CreateShoppingCartResponse : AbstractResponse
    {
        public bool success { get; set; }
        public int id { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class DeleteShoppingCartRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class DeleteShoppingCartResponse : AbstractResponse
    {
        public bool success { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class AddOfferToShoppingCartRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }
        public int offerId { get; set; }
        public int count { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class AddOfferToShoppingCartResponse : AbstractResponse
    {
        public bool success { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class DeleteOfferFromShoppingCartRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }
        public int offerId { get; set; }
        public int count { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class DeleteOfferFromShoppingCartResponse : AbstractResponse
    {
        public bool success { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class CreateOrderFromShoppingCartRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class CreateOrderFromShoppingCartResponse : AbstractResponse
    {
        public Order order { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetAllOrdersRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetAllOrdersResponse : AbstractResponse
    {
        public bool success { get; set; }
        public Order[] orders { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetOrderByIdRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public int orderId { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetOrderByIdResponse : AbstractResponse
    {
        public bool success { get; set; }
        public Order order { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetShopCartByIdRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public int shopCartId { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class GetShopCartByIdResponse : AbstractResponse
    {
        public bool success { get; set; }
        public ShopCart shopCart { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class OfferUpdateSubscriptionRequest : AbstractRequest
    {
        public int id { get; set; }
        public string password { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class OfferUpdateSubscriptionResponse : AbstractResponse
    {
        public bool success { get; set; }

        public override string Serialize() { return Serialize(this); }
    }

    public class OfferUpdateNotification : AbstractResponse
    {
        public Offer offer { get; set; }

        public override string Serialize() { return Serialize(this); }
    }
}
