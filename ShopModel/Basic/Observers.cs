using System;

using Logic = ShopClientLogic.Interface;

namespace ShopModel.Basic
{
    internal class OfferObserver : IObserver<Logic.IOffer>
    {
        Model modelLayer;
        public OfferObserver(Model ml) { modelLayer = ml; }

        public void OnCompleted() { }
        public void OnError(Exception error) { }
        public void OnNext(Logic.IOffer value)
        {
            modelLayer.sync.Post(_ => { modelLayer.RefreshOffers(); }, null);
        }
    }

    internal class OrderObserver : IObserver<Logic.IOrder>
    {
        Model modelLayer;
        public OrderObserver(Model ml) { modelLayer = ml; }

        public void OnCompleted() { }
        public void OnError(Exception error) { }
        public void OnNext(Logic.IOrder value)
        {
            modelLayer.sync.Post(_ => { modelLayer.RefreshOrders(); }, null);
        }
    }

    internal class CartObserver : IObserver<Logic.IShopCart>
    {
        Model modelLayer;
        public CartObserver(Model ml) { modelLayer = ml; }

        public void OnCompleted() { }
        public void OnError(Exception error) { }
        public void OnNext(Logic.IShopCart value)
        {
            modelLayer.sync.Post(_ => { modelLayer.RefreshCarts(); }, null);
        }
    }
}
