using ShopData.Interface;

namespace ShopData.Types
{
    public class Offer : IOffer
    {
        public int sellPrice { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int count { get; set; }

        public object Clone()
        {
            return new Offer
            {
                sellPrice = sellPrice,
                name = name.Clone() as string,
                description = description.Clone() as string,
                count = count
            };
        }
    }
}
