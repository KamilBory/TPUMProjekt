using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int offerCount = 20;

        public MainWindow()
        {
            InitializeComponent();
            UpdateList(OfferPanel);
            this.Hide();
        }

        public void UpdateList(StackPanel stackPanel)
        {
            ViewModel.OfferList offerList = new ViewModel.OfferList(stackPanel);
            for(int i = 0; i < offerCount; i++)
            {
                offerList.AddList(new ViewModel.Offer(i));
            }
            offerList.RefreshList();
        }
    }
}
