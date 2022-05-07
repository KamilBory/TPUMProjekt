using ShopModel.Interface;

namespace ShopModel.Types
{
    public class Offer : IOffer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
    }
}
