using ShopClientLogic.Interface;

namespace ShopClientLogic.Types
{
    public class OfferChoice : IOfferChoice
    {
        public int id { get; set; }
        public int offerId { get; set; }
        public int count { get; set; }
    }
}
