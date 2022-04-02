using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.MemoryModel
{
    public class Order
    {
        public enum State
        {
            WAITING,
            PREPARED,
            SENT,
            FULFILLED,
        }

        private List<OfferChoice> _offers;
        private DeliveryOption _deliveryOption;
        private DateTime _creationTime;
        private State _state;
    }
}
