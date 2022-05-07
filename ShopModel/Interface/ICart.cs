using System.Collections.ObjectModel;

namespace ShopModel.Interface
{
    public interface ICartEntry
    {
        int parentCart { get; set; }
        int choiceId { get; set; }
        int offerId { get; set; }
        string name { get; set; }
        int count { get; set; }
        int price { get; set; }
        int sumPrice { get; set; }
    }

    public interface ICart
    {
        int id { get; set; }
        ObservableCollection<ICartEntry> entries { get; set; }
    }
}
