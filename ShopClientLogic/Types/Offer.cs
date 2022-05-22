using ShopClientLogic.Interface;

namespace ShopClientLogic.Types
{
    public class Offer : IOffer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int count { get; set; }
        public int sellPrice { get; set; }
    }
}
