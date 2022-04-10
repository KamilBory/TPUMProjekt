using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLogic.Interface
{
    public struct ShopCart
    {
        public struct OfferChoice
        {
            public int id;
            public int offerId;
            public int count;
        }

        public int id;
        public int[] offerChoiceIds;
    }
}
