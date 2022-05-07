using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

using ShopModel.Basic;
using ShopModel.Interface;

#pragma warning disable 67

namespace ShopViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IModel modelLayer = Model.GetModelLayer();

        public ICommand AddToCartCommand { get; private set; }
        public ICommand MakeOrderFromCartCommand { get; private set; }
        public ICommand DeleteOneChoiceFromCart { get; private set; }

        public ObservableCollection<IOffer> offers { get => modelLayer.offers; }
        public ObservableCollection<ICart> carts { get => modelLayer.carts; }
        public ObservableCollection<IOrder> orders { get => modelLayer.orders; }

        public ViewModel()
        {
            AddToCartCommand = new RelayCommand<IOffer>(modelLayer.AddToCart);
            MakeOrderFromCartCommand = new RelayCommand<ICart>(modelLayer.MakeOrderFromCart);
            DeleteOneChoiceFromCart = new RelayCommand<ICartEntry>(modelLayer.DeleteOneFromCart);
        }
    }
}
