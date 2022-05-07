using System.Collections.ObjectModel;

using ShopModel.Interface;

namespace ShopModel.Types
{
    public class OrderEntry : IOrderEntry
    {
        public int choiceId { get; set; }
        public int offerId { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public int sumPrice { get; set; }
    }

    public class Order : IOrder
    {
        public int id { get; set; }
        public ObservableCollection<IOrderEntry> entries { get; set; }
    }
}
