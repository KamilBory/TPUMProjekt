using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using ShopPresentation.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ShopPresentation.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ModelLayer modelLayer;

        public ICommand AddToCartCommand { get; private set; }

        public MainWindowViewModel()
        {
            modelLayer.RefreshOffers();
        }

        public ObservableCollection<Offer> offers
        {
            get => modelLayer.offers;
        }

    }
}
