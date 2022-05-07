using ShopData.Interface;

namespace ShopData.Types
{
    public class OfferChoice : IOfferChoice
    {
        public int offerId { get; set; }
        public int count { get; set; }

        public object Clone()
        {
            return new OfferChoice
            {
                offerId = offerId,
                count = count
            };
        }
    }
}
