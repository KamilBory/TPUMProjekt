using System;
using System.Collections.Generic;

namespace ShopLogic.Interface
{
    public interface IOrder
    {
        int id { get; set; }
        int[] offerChoicesIds { get; set; }
        DateTime creationTime { get; set; }
        OrderState state { get; set; }
    }
}
