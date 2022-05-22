using System;
using System.Collections.Generic;
using System.Text;

using ShopClientLogic.Interface;
using ShopClientLogic.Types;
using ShopClientData.Interface;

namespace ShopClientLogic.Basic
{
    internal class ClientLogic : IClientLogic
    {
        private readonly int _currentClientId;
        private readonly string _password;
        IClientData _clientData;

        private List<IObserver<IOffer>> offerObservers = new List<IObserver<IOffer>>();

        public ClientLogic(int clientId, string password, IClientData ctx)
        {
            _currentClientId = clientId;
            _password = password;
            _clientData = ctx;

            _clientData.RegisterObservedMessageCallback(OnOfferUpdate);
            _clientData.RegisterObservedType((int)RequestType.OFFER_OBSERVER);
        }

        public bool AddOfferToShoppingCart(int shopCartId, int offerId, int count)
        {
            var req = new Request<AddOfferToShoppingCartRequest>
            {
                type = RequestType.ADD_OFFER_TO_SHOPCART,
                body = new AddOfferToShoppingCartRequest
                {
                    id = _currentClientId,
                    password = _password,
                    shopCartId = shopCartId,
                    offerId = offerId,
                    count = count,
                },
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<AddOfferToShoppingCartResponse>(resStr);

            return res.success;
        }

        public IOrder CreateOrderFromShoppingCart(int shopCartId)
        {
            var req = new Request<CreateOrderFromShoppingCartRequest>
            {
                type = RequestType.CREATE_ORDER_FROM_SHOPCART,
                body = new CreateOrderFromShoppingCartRequest
                {
                    id = _currentClientId,
                    password = _password,
                    shopCartId = shopCartId
                },
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<CreateOrderFromShoppingCartResponse>(resStr);

            return res.order;
        }

        public int CreateShoppingCart()
        {
            var req = new Request<CreateShoppingCartRequest>
            {
                type = RequestType.CREATE_SHOPCART,
                body = new CreateShoppingCartRequest
                {
                    id = _currentClientId,
                    password = _password,
                },
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<CreateShoppingCartResponse>(resStr);

            return res.id;
        }

        public bool DeleteOfferFromShoppingCart(int shopCartId, int offerId, int count)
        {
            var req = new Request<DeleteOfferFromShoppingCartRequest>
            {
                type = RequestType.DELETE_OFFER_FROM_SHOPCART,
                body = new DeleteOfferFromShoppingCartRequest
                {
                    id = _currentClientId,
                    password = _password,
                    shopCartId = shopCartId,
                    offerId = offerId,
                    count = count,
                },
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<DeleteOfferFromShoppingCartResponse>(resStr);

            return res.success;
        }

        public bool DeleteShoppingCart(int shopCartId)
        {
            var req = new Request<DeleteShoppingCartRequest>
            {
                type = RequestType.DELETE_SHOPCART,
                body = new DeleteShoppingCartRequest
                {
                    id = _currentClientId,
                    password = _password,
                    shopCartId = shopCartId
                },
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<DeleteShoppingCartResponse>(resStr);

            return res.success;
        }

        public IClient Get()
        {
            var req = new Request<GetClientRequest>
            {
                type = RequestType.GET_CLIENT,
                body = new GetClientRequest
                {
                    id = _currentClientId,
                    password = _password,
                },
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<GetClientResponse>(resStr);

            return new Client
            {
                id = res.id,
                name = res.name,
                surname = res.surname,
            };
        }

        public IOffer[] GetAllOffers()
        {
            var req = new Request<GetAllOffersRequest>
            {
                type = RequestType.GET_ALL_OFFERS,
                body = new GetAllOffersRequest
                {
                    id = _currentClientId,
                    password = _password,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<GetAllOffersResponse>(resStr);

            return res.offers;
        }

        public IOrder[] GetAllOrders()
        {
            var req = new Request<GetAllOrdersRequest>
            {
                type = RequestType.GET_ALL_ORDERS,
                body = new GetAllOrdersRequest
                {
                    id = _currentClientId,
                    password = _password,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<GetAllOrdersResponse>(resStr);

            return res.orders;
        }

        public IShopCart[] GetAllShopCarts()
        {
            var req = new Request<GetAllShopCartsRequest>
            {
                type = RequestType.GET_ALL_SHOPCARTS,
                body = new GetAllShopCartsRequest
                {
                    id = _currentClientId,
                    password = _password,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<GetAllShopCartsResponse>(resStr);

            return res.shopCarts;
        }

        public IOffer GetOfferById(int offerId)
        {
            var req = new Request<GetOfferByIdRequest>
            {
                type = RequestType.GET_OFFER_BY_ID,
                body = new GetOfferByIdRequest
                {
                    id = _currentClientId,
                    password = _password,
                    offerId = offerId,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<GetOfferByIdResponse>(resStr);

            return res.offer;
        }

        public IOfferChoice GetOfferChoiceById(int offerChoiceId)
        {
            var req = new Request<GetOfferChoiceByIdRequest>
            {
                type = RequestType.GET_OFFER_CHOICE_BY_ID,
                body = new GetOfferChoiceByIdRequest
                {
                    id = _currentClientId,
                    password = _password,
                    choiceId = offerChoiceId,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<GetOfferChoiceByIdResponse>(resStr);

            return res.offerChoice;
        }

        public IOrder GetOrderById(int orderId)
        {
            var req = new Request<GetOrderByIdRequest>
            {
                type = RequestType.GET_ORDER_BY_ID,
                body = new GetOrderByIdRequest
                {
                    id = _currentClientId,
                    password = _password,
                    orderId = orderId,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<GetOrderByIdResponse>(resStr);

            return res.order;
        }

        public IShopCart GetShopCartById(int shopCartId)
        {
            var req = new Request<GetShopCartByIdRequest>
            {
                type = RequestType.GET_SHOPCART_BY_ID,
                body = new GetShopCartByIdRequest
                {
                    id = _currentClientId,
                    password = _password,
                    shopCartId = shopCartId,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<GetShopCartByIdResponse>(resStr);

            return res.shopCart;
        }

        public bool Update(IClient client)
        {
            var req = new Request<UpdateClientRequest>
            {
                type = RequestType.UPDATE_CLIENT,
                body = new UpdateClientRequest
                {
                    id = client.id,
                    name = client.name,
                    surname = client.surname,
                    password = _password,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<UpdateClientResponse>(resStr);

            return res.success;
        }

        // REACTIVE

        public void OnOfferUpdate(string message)
        {
            UpdateOffer(Serialization.DeserializeComplexResponse<OfferUpdateNotification>(message).offer);
        }

        internal class Unsubscriber<IOffer> : IDisposable
        {
            private List<IObserver<IOffer>> _observers;
            private IObserver<IOffer> _observer;

            internal Unsubscriber(List<IObserver<IOffer>> observers, IObserver<IOffer> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                lock (_observers)
                {
                    if (_observers.Contains(_observer))
                    {
                        _observers.Remove(_observer);
                    }
                }
            }
        }

        public IDisposable SubscribeForOfferUpdate(IObserver<IOffer> observer)
        {
            var req = new Request<OfferUpdateSubscriptionRequest>
            {
                type = RequestType.SUBSCRIBE_FOR_OFFER_UPDATE,
                body = new OfferUpdateSubscriptionRequest
                {
                    id = _currentClientId,
                    password = _password,
                }
            };

            var resStr = _clientData.Interact(Serialization.Serialize(req));

            var res = Serialization.DeserializeResponse<OfferUpdateSubscriptionResponse>(resStr);

            if (!res.success)
            {
                return null;
            }

            lock (offerObservers)
            {
                if (!offerObservers.Contains(observer))
                {
                    offerObservers.Add(observer);
                }

                return new Unsubscriber<IOffer>(offerObservers, observer);
            }
        }

        public void UpdateOffer(IOffer offer)
        {
            lock (offerObservers)
            {
                foreach (var observer in offerObservers)
                {
                    observer.OnNext(offer);
                }
            }
        }
    }
}
