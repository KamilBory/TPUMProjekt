using System.Collections.ObjectModel;

using ShopModel.Interface;

namespace ShopModel.Types
{
    public class CartEntry : ICartEntry
    {
        public int parentCart { get; set; }
        public int choiceId { get; set; }
        public int offerId { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public int sumPrice { get; set; }
    }

    public class Cart : ICart
    {
        public int id { get; set; }
        public ObservableCollection<ICartEntry> entries { get; set; }
    }
}
