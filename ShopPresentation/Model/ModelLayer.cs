using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Logic = ShopLogic.Interface;

namespace ShopPresentation.Model
{
    public class ModelLayer
    {
        public ObservableCollection<Offer> offers { get; private set; }

        private Logic.IClientLogic clientLogic { get; }

        public ModelLayer(Logic.IClientLogic clientLogic)
        {
            this.clientLogic = clientLogic;

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

        internal static Offer ToOffer(Logic.IOffer offer)
        {
            return new Offer
            {
                id = offer.id,
                name = offer.name,
                description = offer.description,
                price = offer.sellPrice,
            };
        }

        public Offer[] ConvertArray(Logic.IOffer[] offers)
        {
            Offer[] offerArray = new Offer[offers.Length];
            for (int i = 0; i < offers.Length; i++)
            {
                offerArray[i] = ToOffer(offers[i]);
            }

            return offerArray;
        }

    }
}
