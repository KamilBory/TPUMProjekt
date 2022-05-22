using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ShopModel.Types;
using ShopModel.Interface;

using Logic = ShopClientLogic.Interface;
using Impl = ShopClientLogic.Basic;

namespace ShopModel.Basic
{
    public class Model : IModel
    {
        public ObservableCollection<IOffer> offers { get; set; }
        public ObservableCollection<IOrder> orders { get; set; }
        public ObservableCollection<ICart> carts { get; set; }

        internal System.Threading.SynchronizationContext sync = new System.Threading.SynchronizationContext();

        private Logic.IClientLogic clientLogic { get; }
        private Logic.ILogic logic { get; }
        private OfferObserver offerObserver;
        private IDisposable offerUnsubscriber;

        private static Model singleton;

        public static Model GetModelLayer()
        {
            if (singleton == null) { singleton = new Model(); }
            return singleton;
        }

        private Model()
        {
            logic = new Impl.Logic();
            clientLogic = logic.GetClientLogic(logic.RegisterClient("Jan", "Nowak", "xd"), "xd");

            offers = new ObservableCollection<IOffer>();
            orders = new ObservableCollection<IOrder>();
            carts = new ObservableCollection<ICart>();

            RefreshOffers();
            RefreshOrders();
            RefreshCarts();

            offerObserver = new OfferObserver(this);
            offerUnsubscriber = clientLogic.SubscribeForOfferUpdate(offerObserver);
        }

        ~Model() { offerUnsubscriber.Dispose(); }

        private IEnumerable<Offer> filteredOffers => Utils.ConvertArray(clientLogic.GetAllOffers());
        private IEnumerable<Order> filteredOrders => Utils.ConvertArray(clientLogic.GetAllOrders(), clientLogic);
        private IEnumerable<Cart> filteredCarts => Utils.ConvertArray(clientLogic.GetAllShopCarts(), clientLogic);

        internal void RefreshOffers()
        {
            lock (offers)
            {
                offers.Clear();
                foreach (Offer offer in filteredOffers) { offers.Add(offer); }
            }
        }

        internal void RefreshOrders()
        {
            lock (orders)
            {
                orders.Clear();
                foreach (var order in filteredOrders) { orders.Add(order); }
            }
        }

        internal void RefreshCarts()
        {
            lock (carts)
            {
                carts.Clear();
                foreach (var cart in filteredCarts) { carts.Add(cart); }
            }
        }

        public void AddToCart(IOffer offer)
        {
            int shopCartId;
            var shopCarts = clientLogic.GetAllShopCarts();

            if (shopCarts.Length == 0)
            {
                shopCartId = clientLogic.CreateShoppingCart();
            }
            else
            {
                shopCartId = shopCarts[0].id;
            }

            try
            {
                clientLogic.AddOfferToShoppingCart(shopCartId, offer.id, 1);
                RefreshCarts();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void MakeOrderFromCart(ICart cart)
        {
            try
            {
                clientLogic.CreateOrderFromShoppingCart(cart.id);
                RefreshCarts();
                RefreshOrders();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void DeleteOneFromCart(ICartEntry cartEntry)
        {
            try
            {
                clientLogic.DeleteOfferFromShoppingCart(cartEntry.parentCart, cartEntry.offerId, 1);
                RefreshCarts();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
