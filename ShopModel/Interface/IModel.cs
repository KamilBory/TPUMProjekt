using System.Collections.ObjectModel;

namespace ShopModel.Interface
{
    public interface IModel
    {
        ObservableCollection<IOffer> offers { get; set; }
        ObservableCollection<IOrder> orders { get; set; }
        ObservableCollection<ICart> carts { get; set; }

        void AddToCart(IOffer offer);
        void MakeOrderFromCart(ICart cart);
        void DeleteOneFromCart(ICartEntry cartEntry);
    }
}
