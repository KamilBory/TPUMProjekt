using System;

namespace ShopData.Interface
{
    public interface IOfferChoice : ICloneable
    {
        int offerId { get; set; }
        int count { get; set; }
    }
}
