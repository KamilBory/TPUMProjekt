using ShopData.Interface;

namespace ShopData.Types
{
    public class OfferChoice : IOfferChoice
    {
        public int offerId { get; set; }
        public int count { get; set; }
    }
}
