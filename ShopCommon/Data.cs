using System;

namespace ShopCommon.Data
{
    public enum OrderState
    {
        WAITING,
        PREPARED,
        SENT,
        FULFILLED,
    }

    public struct Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }

    public struct Offer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int count { get; set; }
        public int sellPrice { get; set; }
    }

    public struct OfferChoice
    {
        public int id { get; set; }
        public int offerId { get; set; }
        public int count { get; set; }
    }

    public struct Order
    {
        public int id { get; set; }
        public int[] offerChoicesIds { get; set; }
        public DateTime creationTime { get; set; }
        public OrderState state { get; set; }
    }

    public struct ShopCart
    {
        public int id { get; set; }
        public int[] offerChoiceIds { get; set; }
    }
}
