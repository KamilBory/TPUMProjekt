using System;
using System.Text.Json;
using ShopServerPresentation.Calls;

namespace ShopServerPresentation
{
    public static class Serialization
    {
        public delegate string DeserializedCall(WebSocketConnection wsc);

        public static DeserializedCall Deserialize(string message)
        {
            var callBase = JsonSerializer.Deserialize<RequestBase>(message);

            switch (callBase.type)
            {
                case RequestType.GET_CLIENT:
                    return DeserializeDetailed<GetClientRequest>(message);
                case RequestType.UPDATE_CLIENT:
                    return DeserializeDetailed<UpdateClientRequest>(message);
                case RequestType.REGISTER_CLIENT:
                    return DeserializeDetailed<RegisterClientRequest>(message);
                case RequestType.GET_ALL_OFFERS:
                    return DeserializeDetailed<GetAllOffersRequest>(message);
                case RequestType.GET_OFFER_BY_ID:
                    return DeserializeDetailed<GetOfferByIdRequest>(message);
                case RequestType.GET_OFFER_CHOICE_BY_ID:
                    return DeserializeDetailed<GetOfferChoiceByIdRequest>(message);
                case RequestType.GET_ALL_SHOPCARTS:
                    return DeserializeDetailed<GetAllShopCartsRequest>(message);
                case RequestType.CREATE_SHOPCART:
                    return DeserializeDetailed<CreateShoppingCartRequest>(message);
                case RequestType.DELETE_SHOPCART:
                    return DeserializeDetailed<DeleteShoppingCartRequest>(message);
                case RequestType.ADD_OFFER_TO_SHOPCART:
                    return DeserializeDetailed<AddOfferToShoppingCartRequest>(message);
                case RequestType.DELETE_OFFER_FROM_SHOPCART:
                    return DeserializeDetailed<DeleteOfferFromShoppingCartRequest>(message);
                case RequestType.CREATE_ORDER_FROM_SHOPCART:
                    return DeserializeDetailed<CreateOrderFromShoppingCartRequest>(message);
                case RequestType.GET_ALL_ORDERS:
                    return DeserializeDetailed<GetAllOrdersRequest>(message);
                case RequestType.GET_ORDER_BY_ID:
                    return DeserializeDetailed<GetOrderByIdRequest>(message);
                case RequestType.GET_SHOPCART_BY_ID:
                    return DeserializeDetailed<GetShopCartByIdRequest>(message);
                case RequestType.SUBSCRIBE_FOR_OFFER_UPDATE:
                    return DeserializeDetailed<OfferUpdateSubscriptionRequest>(message);
                case RequestType.OFFER_OBSERVER:
                    throw new NotImplementedException("Shouldn't ever receive offer observer");
                default:
                    throw new NotImplementedException();
            }
        }

        public static DeserializedCall DeserializeDetailed<T>(string message) where T : AbstractRequest
        {
            var call = JsonSerializer.Deserialize<Request<T>>(message);

            return (WebSocketConnection wsc) =>
            {
                return call.body.Execute(wsc);
            };
        }

        public static T DeserializeResponse<T>(string message) where T : AbstractResponse
        {
            return JsonSerializer.Deserialize<T>(message);
        }
    }
}
