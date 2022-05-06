using ShopData.Interface;

namespace ShopData.Types
{
    public class Offer : IOffer
    {
        public int sellPrice { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int count { get; set; }
    }
}
