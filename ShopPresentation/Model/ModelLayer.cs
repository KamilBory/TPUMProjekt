using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Logic = ShopLogic.Interface;
using LogicImpl = ShopLogic.Basic;
using System.ComponentModel;

namespace ShopPresentation.Model
{
    public class ModelLayer
    {
        public ObservableCollection<Offer> offers { get; set; }
        public ObservableCollection<Order> orders { get; set; }
        public ObservableCollection<Cart> carts { get; set; }

        internal System.Threading.SynchronizationContext sync = new System.Threading.SynchronizationContext();

        private Logic.IClientLogic clientLogic { get; }
        private Logic.ILogic logic { get; }
        private OfferObserver offerObserver;
        private IDisposable offerUnsubscriber;

        private static ModelLayer singleton;

        public static ModelLayer GetModelLayer()
        {
            if (singleton == null) { singleton = new ModelLayer(); }
            return singleton;
        }

        private ModelLayer()
        {
            logic = new LogicImpl.Logic();
            clientLogic = logic.GetClientLogic(logic.RegisterClient("Jan", "Nowak", "xd"), "xd");

            offers = new ObservableCollection<Offer>();
            orders = new ObservableCollection<Order>();
            carts = new ObservableCollection<Cart>();

            RefreshOffers();
            RefreshOrders();
            RefreshCarts();

            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(offers, offers);

            offerObserver = new OfferObserver(this);
            offerUnsubscriber = clientLogic.SubscribeForOfferUpdate(offerObserver);
        }

        ~ModelLayer() { offerUnsubscriber.Dispose(); }

        public IEnumerable<Offer> filteredOffers => Utils.ConvertArray(clientLogic.GetAllOffers());
        public IEnumerable<Order> filteredOrders => Utils.ConvertArray(clientLogic.GetAllOrders(), clientLogic);
        public IEnumerable<Cart> filteredCarts => Utils.ConvertArray(clientLogic.GetAllShopCarts(), clientLogic);

        public void RefreshOffers()
        {
            lock (offers)
            {
                offers.Clear();
                foreach (Offer offer in filteredOffers) { offers.Add(offer); }
            }
        }

        public void RefreshOrders()
        {
            lock (orders)
            {
                orders.Clear();
                foreach (var order in filteredOrders) { orders.Add(order); }
            }
        }

        public void RefreshCarts()
        {
            lock (carts)
            {
                carts.Clear();
                foreach (var cart in filteredCarts) { carts.Add(cart); }
            }
        }

        public void AddToCart(Offer offer)
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

        public void MakeOrderFromCart(Cart cart)
        {
            try
            {
                clientLogic.CreateOrderFromShoppingCart(cart.id);
                RefreshCarts();
                RefreshOrders();
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void DeleteOneFromCart(Cart.Entry cartEntry)
        {
            try
            {
                clientLogic.DeleteOfferFromShoppingCart(cartEntry.parentCart, cartEntry.offerId, 1);
                RefreshCarts();
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
