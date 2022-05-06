using System;
using ShopLogic.Interface;

namespace ShopLogic.Types
{
    public class Order : IOrder
    {
        public int id { get; set; }
        public int[] offerChoicesIds { get; set; }
        public DateTime creationTime { get; set; }
        public OrderState state { get; set; }
    }
}
