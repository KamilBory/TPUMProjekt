using System;
using System.Collections.Generic;

namespace ShopData.Interface
{
    public struct Order
    {
        private HashSet<int> offerChoiceIds;
        private int deliveryOptionId;
        private DateTime creationTime;
        private OrderState state;
    }
}
