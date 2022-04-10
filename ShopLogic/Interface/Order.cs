using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLogic.Interface
{
    public struct Order
    {
        public enum State
        {
            WAITING,
            PREPARED,
            SENT,
            FULFILLED,
        }

        public int id;
        public int[] offerChoicesIds;
        public int deliveryOptionId;
        public State state;
    }
}
