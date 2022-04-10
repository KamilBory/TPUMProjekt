using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShopPresentation.ViewModel
{
    class CartList
    {
        private List<ItemInCart> cart;
        private StackPanel stackPanel;

        public CartList(StackPanel stackP)
        {
            stackPanel = stackP;
            cart = new List<ItemInCart>();
        }

        public void RefreshList()
        {
            foreach (ItemInCart o in cart)
            {
                stackPanel.Children.Add(o.GetGrid());
            }
        }

        public void ClearList()
        {
            cart.Clear();
        }

        public void AddList(ItemInCart o)
        {
            cart.Add(o);
        }
    }
}
