using System.Text.Json;
using ShopCommon.Calls;

namespace ShopServerPresentation
{
    public static class Serialization
    {
        private static string ser(object o) { return JsonSerializer.Serialize(o); }

        private static T des<T>(string o) { return JsonSerializer.Deserialize<T>(o); }

        public static string ContextCall(WebSocketConnection wsc, string message)
        {
            var messageBase = JsonSerializer.Deserialize<MessageBase>(message);

            switch (messageBase.type)
            {
                case Type.GET_CLIENT:
                    return ser(Handlers.GetClient(wsc, des<GetClientRequest>(message)));
                case Type.UPDATE_CLIENT:
                    return ser(Handlers.UpdateClient(wsc, des<UpdateClientRequest>(message)));
                case Type.REGISTER_CLIENT:
                    return ser(Handlers.RegisterClient(wsc, des<RegisterClientRequest>(message)));
                case Type.GET_ALL_OFFERS:
                    return ser(Handlers.GetAllOffers(wsc, des<GetAllOffersRequest>(message)));
                case Type.GET_OFFER_BY_ID:
                    return ser(Handlers.GetOfferById(wsc, des<GetOfferByIdRequest>(message)));
                case Type.GET_OFFER_CHOICE_BY_ID:
                    return ser(Handlers.GetOfferChoiceById(wsc, des<GetOfferChoiceByIdRequest>(message)));
                case Type.GET_ALL_SHOPCARTS:
                    return ser(Handlers.GetAllShopCarts(wsc, des<GetAllShopCartsRequest>(message)));
                case Type.CREATE_SHOPCART:
                    return ser(Handlers.CreateShoppingCart(wsc, des<CreateShoppingCartRequest>(message)));
                case Type.DELETE_SHOPCART:
                    return ser(Handlers.DeleteShoppingCart(wsc, des<DeleteShoppingCartRequest>(message)));
                case Type.ADD_OFFER_TO_SHOPCART:
                    return ser(Handlers.AddOfferToShoppingCart(wsc, des<AddOfferToShoppingCartRequest>(message)));
                case Type.DELETE_OFFER_FROM_SHOPCART:
                    return ser(Handlers.DeleteOfferFromShoppingCart(wsc, des<DeleteOfferFromShoppingCartRequest>(message)));
                case Type.CREATE_ORDER_FROM_SHOPCART:
                    return ser(Handlers.CreateOrderFromShoppingCart(wsc, des<CreateOrderFromShoppingCartRequest>(message)));
                case Type.GET_ALL_ORDERS:
                    return ser(Handlers.GetAllOrders(wsc, des<GetAllOrdersRequest>(message)));
                case Type.GET_ORDER_BY_ID:
                    return ser(Handlers.GetOrderById(wsc, des<GetOrderByIdRequest>(message)));
                case Type.GET_SHOPCART_BY_ID:
                    return ser(Handlers.GetShopCartById(wsc, des<GetShopCartByIdRequest>(message)));
                case Type.SUBSCRIBE_FOR_OFFER_UPDATE:
                    return ser(Handlers.SubscribeForOfferUpdate(wsc, des<OfferUpdateSubscriptionRequest>(message)));
                case Type.OFFER_OBSERVER:
                    throw new System.NotImplementedException("Shouldn't ever receive offer observer");
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}
