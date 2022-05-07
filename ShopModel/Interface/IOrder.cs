using System.Collections.ObjectModel;

namespace ShopModel.Interface
{
    public interface IOrderEntry
    {
        int choiceId { get; set; }
        int offerId { get; set; }
        string name { get; set; }
        int count { get; set; }
        int price { get; set; }
        int sumPrice { get; set; }
    }

    public interface IOrder
    {
        int id { get; set; }
        ObservableCollection<IOrderEntry> entries { get; set; }
    }
}
