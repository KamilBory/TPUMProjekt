using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Logic = ShopLogic.Interface;

namespace ShopPresentation.ViewModel
{
    class OfferList
    {
        private List<Offer> offers;
        private StackPanel stackPanel;
        private Logic.ILogic logic;

        public OfferList(StackPanel stackP)
        {
            stackPanel = stackP;
            offers = new List<Offer>();
        }

        public void RefreshList()
        {
            foreach(Offer o in offers)
            {
                stackPanel.Children.Add(o.GetGrid());
            }
        }

        public void ClearList()
        {
            offers.Clear();
        }

        public void AddList(Offer o)
        {
            offers.Add(o);
        }
    }
}
