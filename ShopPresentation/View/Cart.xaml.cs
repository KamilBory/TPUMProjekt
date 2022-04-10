using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShopPresentation.View
{
    /// <summary>
    /// Logika interakcji dla klasy Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        private int offerCount = 3;
        public Cart()
        {
            InitializeComponent();
            UpdateList(CurrentPanel);
            this.Show();
        }

        public void UpdateList(StackPanel stackPanel)
        {
            ViewModel.CartList cartList = new ViewModel.CartList(stackPanel);
            for (int i = 0; i < offerCount; i++)
            {
                cartList.AddList(new ViewModel.ItemInCart(i));
            }
            cartList.RefreshList();
        }
    }
}
