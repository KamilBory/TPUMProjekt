using System;
using System.Collections.Generic;

namespace ShopData.Interface
{
    public interface IShopCart : ICloneable
    {
        int clientId { get; set; }
        HashSet<int> offerChoiceIds { get; set; }
    }
}
