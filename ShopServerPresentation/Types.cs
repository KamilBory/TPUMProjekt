using System;
using System.Collections.Generic;
using System.Text;

namespace ShopServerPresentation.Types
{
    [Serializable]
    public enum OrderState
    {
        WAITING,
        PREPARED,
        SENT,
        FULFILLED,
    }

    [Serializable]
    struct Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }

    [Serializable]
    struct Offer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int count { get; set; }
        public int sellPrice { get; set; }
    }

    [Serializable]
    struct OfferChoice
    {
        public int id { get; set; }
        public int offerId { get; set; }
        public int count { get; set; }
    }

    [Serializable]
    struct Order
    {
        public int id { get; set; }
        public int[] offerChoicesIds { get; set; }
        public DateTime creationTime { get; set; }
        public OrderState state { get; set; }
    }

    [Serializable]
    struct ShopCart
    {
        public int id { get; set; }
        public int[] offerChoiceIds { get; set; }
    }
}
