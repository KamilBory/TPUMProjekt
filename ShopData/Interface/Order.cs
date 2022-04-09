using System;
using System.Collections.Generic;

namespace ShopData.Interface
{
    public struct Order
    {
        public int clientId;
        public HashSet<int> offerChoiceIds;
        public int deliveryOptionId;
        public DateTime creationTime;
        public OrderState state;
    }
}
