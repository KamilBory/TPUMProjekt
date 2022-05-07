using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using LogicInterface = ShopLogic.Interface;
using ShopLogic.Basic;

namespace ShopPresentation.Model
{
    public class ModelLayer
    {
        public ObservableCollection<Offer> offers { get; private set; }

        private LogicInterface.IClientLogic clientLogic { get; }

        private LogicInterface.ILogic logic { get; }

        public ModelLayer()
        {
            logic = new Logic();
            int id = logic.RegisterClient("Jan", "Nowak", "xd");
            clientLogic = logic.GetClientLogic(id, "xd");

            offers = new ObservableCollection<Offer>();
        }

        public IEnumerable<Offer> filteredOffers => ConvertArray(clientLogic.GetAllOffers());

        public void RefreshOffers()
        {
            offers.Clear();
            foreach (Offer offer in filteredOffers)
            {
                offers.Add(offer);
            }
        }

        internal static Offer ToOffer(LogicInterface.IOffer offer)
        {
            return new Offer
            {
                id = offer.id,
                name = offer.name,
                description = offer.description,
                price = offer.sellPrice,
            };
        }

        public Offer[] ConvertArray(LogicInterface.IOffer[] offers)
        {
            Offer[] offerArray = new Offer[offers.Length];
            for (int i = 0; i < offers.Length; i++)
            {
                offerArray[i] = ToOffer(offers[i]);
            }

            return offerArray;
        }

        public void AddToCart()
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


        }

    }
}
