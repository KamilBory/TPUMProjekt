using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using ShopPresentation.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ShopPresentation.ViewModel.Commands;

namespace ShopPresentation.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ModelLayer modelLayer = ModelLayer.GetModelLayer();

        public ICommand AddToCartCommand { get; private set; }
        public ICommand MakeOrderFromCartCommand { get; private set; }
        public ICommand DeleteOneChoiceFromCart { get; private set; }

        public ObservableCollection<Offer> offers { get => modelLayer.offers; }
        public ObservableCollection<Cart> carts { get => modelLayer.carts; }
        public ObservableCollection<Order> orders { get => modelLayer.orders; }

        public MainWindowViewModel()
        {
            AddToCartCommand = new RelayCommand<Offer>(modelLayer.AddToCart);
            MakeOrderFromCartCommand = new RelayCommand<Cart>(modelLayer.MakeOrderFromCart);
            DeleteOneChoiceFromCart = new RelayCommand<Cart.Entry>(modelLayer.DeleteOneFromCart);
        }
    }
}
