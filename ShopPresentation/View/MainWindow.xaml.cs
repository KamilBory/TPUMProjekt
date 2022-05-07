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
            OfferScrollViewer.Visibility = Visibility.Hidden;
            CartScrollViewer.Visibility = Visibility.Hidden;
            OrderScrollViewer.Visibility = Visibility.Visible;

            OfferScrollViewer.IsEnabled = false;
            CartScrollViewer.IsEnabled = false;
            OrderScrollViewer.IsEnabled = true;
        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            OfferScrollViewer.Visibility = Visibility.Hidden;
            CartScrollViewer.Visibility = Visibility.Visible;
            OrderScrollViewer.Visibility = Visibility.Hidden;

            OfferScrollViewer.IsEnabled = false;
            CartScrollViewer.IsEnabled = true;
            OrderScrollViewer.IsEnabled = false;
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            OfferScrollViewer.Visibility = Visibility.Visible;
            CartScrollViewer.Visibility = Visibility.Hidden;
            OrderScrollViewer.Visibility = Visibility.Hidden;

            OfferScrollViewer.IsEnabled = true;
            CartScrollViewer.IsEnabled = false;
            OrderScrollViewer.IsEnabled = false;
        }
    }
}
