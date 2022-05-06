using Data = ShopData.Interface;

using ShopLogic.Interface;
using ShopLogic.Types;

using System;
using System.Collections.Generic;

namespace ShopLogic.Basic
{
    public class ClientLogic : IClientLogic
    {
        private readonly int _currentClientId;
        private readonly string _password;

        private Data.IDatabase _database;
        private List<IObserver<IOffer>> offerObservers = new List<IObserver<IOffer>>();

        public ClientLogic(int clientId, string password, Data.IDatabase database)
        {
            _currentClientId = clientId;
            _database = database;
            _password = password;

            var clientRepo = _database.GetClientRepo();

            Conc.LockExec(new object[] { clientRepo }, () =>
            {
                var dbClient = clientRepo.Get(_currentClientId) ?? throw new Exception("Invalid client id");

                if (dbClient.password != password) { throw new Exception("Invalid password"); }
            });
        }

        public IOffer[] GetAllOffers()
        {
            var offerRepo = _database.GetOfferRepo();

            return Conc.LockExec(new object[] { offerRepo }, () =>
            {
                var dbOffers = offerRepo.List();
                var offers = new Offer[dbOffers.Length];

                for (int i = 0; i < dbOffers.Length; ++i)
                {
                    offers[i] = Utilities.Convert(dbOffers[i].Value, dbOffers[i].Key);
                }

                return offers;
            });
        }

        public IOffer GetOfferById(int offerId)
        {
            var offerRepo = _database.GetOfferRepo();

            return Conc.LockExec(new object[] { offerRepo }, () =>
            {
                var dbOffer = offerRepo.Get(offerId) ?? throw new Exception("Invalid offer id");

                return Utilities.Convert(dbOffer, offerId);
            });
        }

        public IShopCart[] GetAllShopCarts()
        {
            var shopCartRepo = _database.GetShopCartRepo();

            return Conc.LockExec(new object[] { shopCartRepo }, () =>
            {
                var dbShopCarts = shopCartRepo.List();
                Utilities.Filter(ref dbShopCarts, _currentClientId);

                return Utilities.Convert(dbShopCarts);
            });
        }

        public int CreateShoppingCart()
        {
            var shopCartRepo = _database.GetShopCartRepo();

            return Conc.LockExec(new object[] { shopCartRepo }, () =>
            {
                var newDbShopCart = _database.CreateShopCart(_currentClientId, new HashSet<int>());
                return shopCartRepo.Create(newDbShopCart);
            });
        }

        public void DeleteShoppingCart(int shopCartId)
        {
            var shopCartRepo = _database.GetShopCartRepo();

            Conc.LockExec(new object[] { shopCartRepo }, () =>
            {
                if (!shopCartRepo.Delete(shopCartId)) { throw new Exception("Failed to delete shop cart"); }
            });
        }

        public void AddOfferToShoppingCart(int shopCartId, int offerId, int count)
        {
            if (count <= 0) throw new Exception("Invalid count parameter");

            var offerRepo = _database.GetOfferRepo();
            var offerChoiceRepo = _database.GetOfferChoiceRepo();
            var shopCartRepo = _database.GetShopCartRepo();

            Conc.LockExec(new object[] { offerRepo, offerChoiceRepo, shopCartRepo }, () =>
            {
                var dbOffer = offerRepo.Get(offerId) ?? throw new Exception("Invalid offer id");
                var dbShopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");

                foreach (var dbOfferChoiceId in dbShopCart.offerChoiceIds)
                {
                    var dbOfferChoice = offerChoiceRepo.Get(dbOfferChoiceId) ?? throw new Exception("Invalid offer choice id");

                    if (dbOfferChoice.offerId != offerId) { continue; }

                    dbOfferChoice.count += count;

                    if (dbOfferChoice.count > dbOffer.count) { throw new Exception("Tried to add more than available"); }

                    if (!offerChoiceRepo.Update(dbOfferChoiceId, dbOfferChoice)) { throw new Exception("Failed to update offer choice"); }

                    return;
                }

                var newDbOfferChoice = _database.CreateOfferChoice(offerId, count);

                if (newDbOfferChoice.count > dbOffer.count) { throw new Exception("Tried to add more than available"); }

                var newDbOfferChoiceId = offerChoiceRepo.Create(newDbOfferChoice);

                dbShopCart.offerChoiceIds.Add(newDbOfferChoiceId);

                if (!shopCartRepo.Update(shopCartId, dbShopCart)) { throw new Exception("Failed to update shop cart"); }
            });
        }

        public void DeleteOfferFromShoppingCart(int shopCartId, int offerId, int count)
        {
            if (count < 0) throw new Exception("Invalid count parameter");

            var shopCartRepo = _database.GetShopCartRepo();
            var offerChoiceRepo = _database.GetOfferChoiceRepo();

            Conc.LockExec(new object[] { offerChoiceRepo, shopCartRepo }, () =>
            {
                var dbShopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");

                foreach (var dbOfferChoiceId in dbShopCart.offerChoiceIds)
                {
                    var dbOfferChoice = offerChoiceRepo.Get(dbOfferChoiceId) ?? throw new Exception("Invalid offer choice id");

                    if (dbOfferChoice.offerId != offerId) { continue; }

                    dbOfferChoice.count -= count;

                    if (dbOfferChoice.count <= 0 || count == 0)
                    {
                        if (!offerChoiceRepo.Delete(dbOfferChoiceId)) { throw new Exception("Failed to delete offer choice"); }
                        dbShopCart.offerChoiceIds.Remove(dbOfferChoiceId);
                        if (!shopCartRepo.Update(shopCartId, dbShopCart)) { throw new Exception("Failed to update shop cart"); }
                    }
                    else
                    {
                        if (!offerChoiceRepo.Update(dbOfferChoiceId, dbOfferChoice)) { throw new Exception("Failed to update offer choice"); }
                    }

                    return;
                }

                throw new Exception("Invalid offer id");
            });
        }

        public IOrder CreateOrderFromShoppingCart(int shopCartId)
        {
            var orderRepo = _database.GetOrderRepo();
            var shopCartRepo = _database.GetShopCartRepo();

            return Conc.LockExec(new object[] { orderRepo, shopCartRepo }, () =>
            {
                var dbShopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");

                var newDbOrder = _database.CreateOrder(_currentClientId, dbShopCart.offerChoiceIds, DateTime.UtcNow, Data.OrderState.WAITING);

                var offerChoicesIds = new int[dbShopCart.offerChoiceIds.Count];
                dbShopCart.offerChoiceIds.CopyTo(offerChoicesIds);

                var newDbOrderId = orderRepo.Create(newDbOrder);

                shopCartRepo.Delete(shopCartId);

                return new Order
                {
                    id = newDbOrderId,
                    offerChoicesIds = offerChoicesIds,
                    state = Utilities.Convert(newDbOrder.state)
                };
            });
        }

        public IOrder[] GetAllOrders()
        {
            var orderRepo = _database.GetOrderRepo();

            return Conc.LockExec(new object[] { orderRepo }, () =>
            {
                var dbOrders = orderRepo.List();
                Utilities.Filter(ref dbOrders, _currentClientId);

                return Utilities.Convert(dbOrders);
            });
        }

        public IOrder GetOrderById(int orderId)
        {
            var orderRepo = _database.GetOrderRepo();

            return Conc.LockExec(new object[] { orderRepo }, () =>
            {
                var dbOrder = orderRepo.Get(orderId) ?? throw new Exception("Invalid order id");
                return Utilities.Convert(dbOrder, orderId);
            });
        }

        public IOfferChoice GetOfferChoiceById(int offerChoiceId)
        {
            var offerChoiceRepo = _database.GetOfferChoiceRepo();

            return Conc.LockExec(new object[] { offerChoiceRepo }, () =>
            {
                var dbOfferChoice = offerChoiceRepo.Get(offerChoiceId) ?? throw new Exception("Invalid offer choice id");
                return Utilities.Convert(dbOfferChoice, offerChoiceId);
            });
        }

        public IShopCart GetShopCartById(int shopCartId)
        {
            var shopCartRepo = _database.GetShopCartRepo();

            return Conc.LockExec(new object[] { shopCartRepo }, () =>
            {
                var shopCart = shopCartRepo.Get(shopCartId) ?? throw new Exception("Invalid shop cart id");
                return Utilities.Convert(shopCart, shopCartId);
            });
        }

        public IClient Get()
        {
            var clientRepo = _database.GetClientRepo();

            return Conc.LockExec(new object[] { clientRepo }, () =>
            {
                var dbClient = clientRepo.Get(_currentClientId) ?? throw new Exception("Invalid client id");
                return Utilities.Convert(dbClient, _currentClientId);
            });
        }

        public void Update(IClient client)
        {
            var clientRepo = _database.GetClientRepo();

            Conc.LockExec(new object[] { clientRepo }, () =>
            {
                if (!clientRepo.Update(_currentClientId, Utilities.Convert(client, _password, _database))) { throw new Exception("Failed to update client"); }
            });
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
                Conc.LockExec(new object[] { _observers }, () =>
                {
                    if (_observers.Contains(_observer))
                    {
                        _observers.Remove(_observer);
                    }
                });
            }
        }

        public IDisposable SubscribeForOfferUpdate(IObserver<IOffer> observer)
        {
            return Conc.LockExec(new object[] { observer }, () =>
            {
                if (!offerObservers.Contains(observer))
                {
                    offerObservers.Add(observer);
                }

                return new Unsubscriber<IOffer>(offerObservers, observer);
            });
        }

        public void UpdateOffer(IOffer offer)
        {
            Conc.LockExec(new object[] { offerObservers }, () =>
            {
                foreach (var observer in offerObservers)
                {
                    observer.OnNext(offer);
                }
            });
        }
    }
}
