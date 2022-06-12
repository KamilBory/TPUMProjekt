using System.Collections.Generic;
using System.Text;
using System;

using ShopClientLogic.Interface;
using ShopClientLogic.Types;
using ShopClientData.Interface;

using ShopCommon.Calls;

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
            _clientData.RegisterObservedType((int)ShopCommon.Calls.Type.OFFER_OBSERVER);
        }

        public bool AddOfferToShoppingCart(int shopCartId, int offerId, int count)
        {
            var req = new AddOfferToShoppingCartRequest
            {
                id = _currentClientId,
                password = _password,
                shopCartId = shopCartId,
                offerId = offerId,
                count = count,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<AddOfferToShoppingCartResponse>(resStr);

            return res.success;
        }

        public IOrder CreateOrderFromShoppingCart(int shopCartId)
        {
            var req = new CreateOrderFromShoppingCartRequest
            {
                id = _currentClientId,
                password = _password,
                shopCartId = shopCartId
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<CreateOrderFromShoppingCartResponse>(resStr);

            return Utils.Conv(res.order);
        }

        public int CreateShoppingCart()
        {
            var req = new CreateShoppingCartRequest
            {
                id = _currentClientId,
                password = _password,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<CreateShoppingCartResponse>(resStr);

            return res.id;
        }

        public bool DeleteOfferFromShoppingCart(int shopCartId, int offerId, int count)
        {
            var req = new DeleteOfferFromShoppingCartRequest
            {
                id = _currentClientId,
                password = _password,
                shopCartId = shopCartId,
                offerId = offerId,
                count = count,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<DeleteOfferFromShoppingCartResponse>(resStr);

            return res.success;
        }

        public bool DeleteShoppingCart(int shopCartId)
        {
            var req = new DeleteShoppingCartRequest
            {
                id = _currentClientId,
                password = _password,
                shopCartId = shopCartId
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<DeleteShoppingCartResponse>(resStr);

            return res.success;
        }

        public IClient Get()
        {
            var req = new GetClientRequest
            {
                id = _currentClientId,
                password = _password,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<GetClientResponse>(resStr);

            return new Client
            {
                id = res.id,
                name = res.name,
                surname = res.surname,
            };
        }

        public IOffer[] GetAllOffers()
        {
            var req = new GetAllOffersRequest
            {
                id = _currentClientId,
                password = _password,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<GetAllOffersResponse>(resStr);

            return Utils.Conv(res.offers);
        }

        public IOrder[] GetAllOrders()
        {
            var req = new GetAllOrdersRequest
            {
                id = _currentClientId,
                password = _password,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<GetAllOrdersResponse>(resStr);

            return Utils.Conv(res.orders);
        }

        public IShopCart[] GetAllShopCarts()
        {
            var req = new GetAllShopCartsRequest
            {
                id = _currentClientId,
                password = _password,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<GetAllShopCartsResponse>(resStr);

            return Utils.Conv(res.shopCarts);
        }

        public IOffer GetOfferById(int offerId)
        {
            var req = new GetOfferByIdRequest
            {
                id = _currentClientId,
                password = _password,
                offerId = offerId,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<GetOfferByIdResponse>(resStr);

            return Utils.Conv(res.offer);
        }

        public IOfferChoice GetOfferChoiceById(int offerChoiceId)
        {
            var req = new GetOfferChoiceByIdRequest
            {
                id = _currentClientId,
                password = _password,
                choiceId = offerChoiceId,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<GetOfferChoiceByIdResponse>(resStr);

            return Utils.Conv(res.offerChoice);
        }

        public IOrder GetOrderById(int orderId)
        {
            var req = new GetOrderByIdRequest
            {
                id = _currentClientId,
                password = _password,
                orderId = orderId,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<GetOrderByIdResponse>(resStr);

            return Utils.Conv(res.order);
        }

        public IShopCart GetShopCartById(int shopCartId)
        {
            var req = new GetShopCartByIdRequest
            {
                id = _currentClientId,
                password = _password,
                shopCartId = shopCartId,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<GetShopCartByIdResponse>(resStr);

            return Utils.Conv(res.shopCart);
        }

        public bool Update(IClient client)
        {
            var req = new UpdateClientRequest
            {
                id = client.id,
                name = client.name,
                surname = client.surname,
                password = _password,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<UpdateClientResponse>(resStr);

            return res.success;
        }

        // REACTIVE

        public void OnOfferUpdate(string message)
        {
            UpdateOffer(Utils.Conv(Utils.Des<OfferUpdateNotification>(message).offer));
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
            var req = new OfferUpdateSubscriptionRequest
            {
                id = _currentClientId,
                password = _password,
            };

            var resStr = _clientData.Interact(Utils.Ser(req));

            var res = Utils.Des<OfferUpdateSubscriptionResponse>(resStr);

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
