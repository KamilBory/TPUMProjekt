using System.Windows;

namespace ShopView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var offers = (DataContext as ShopViewModel.ViewModel).offers;
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(offers, offers);
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
