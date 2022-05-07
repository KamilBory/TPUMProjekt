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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CartPanel.Visibility = Visibility.Hidden;
            this.OfferPanel.Visibility = Visibility.Visible;
        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CartPanel.Visibility = Visibility.Visible;
            this.OfferPanel.Visibility = Visibility.Hidden;
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CartPanel.Visibility = Visibility.Hidden;
            this.OfferPanel.Visibility = Visibility.Hidden;
        }
    }
}
