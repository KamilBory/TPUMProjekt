using System;
using System.Collections.Generic;
using ShopData.Interface;

namespace ShopData.Types
{
    public class Order : IOrder
    {
        public int clientId { get; set; }
        public HashSet<int> offerChoiceIds { get; set; }
        public DateTime creationTime { get; set; }
        public OrderState state { get; set; }
    }
}
