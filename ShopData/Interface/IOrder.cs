using System;
using System.Collections.Generic;

namespace ShopData.Interface
{
    public interface IOrder : ICloneable
    {
        int clientId { get; set; }
        HashSet<int> offerChoiceIds { get; set; }
        DateTime creationTime { get; set; }
        OrderState state { get; set; }
    }
}
