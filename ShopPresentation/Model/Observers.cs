using System;
using System.Collections.Generic;
using System.Text;

using Logic = ShopLogic.Interface;

namespace ShopPresentation.Model
{
    internal class OfferObserver : IObserver<Logic.IOffer>
    {
        ModelLayer modelLayer;
        public OfferObserver(ModelLayer ml) { modelLayer = ml; }

        public void OnCompleted() { }
        public void OnError(Exception error) { }
        public void OnNext(Logic.IOffer value)
        {
            modelLayer.sync.Post(_ => { modelLayer.RefreshOffers(); }, null);
        }
    }

    internal class OrderObserver : IObserver<Logic.IOrder>
    {
        ModelLayer modelLayer;
        public OrderObserver(ModelLayer ml) { modelLayer = ml; }

        public void OnCompleted() { }
        public void OnError(Exception error) { }
        public void OnNext(Logic.IOrder value)
        {
            modelLayer.sync.Post(_ => { modelLayer.RefreshOrders(); }, null);
        }
    }

    internal class CartObserver : IObserver<Logic.IShopCart>
    {
        ModelLayer modelLayer;
        public CartObserver(ModelLayer ml) { modelLayer = ml; }

        public void OnCompleted() { }
        public void OnError(Exception error) { }
        public void OnNext(Logic.IShopCart value)
        {
            modelLayer.sync.Post(_ => { modelLayer.RefreshCarts(); }, null);
        }
    }
}
