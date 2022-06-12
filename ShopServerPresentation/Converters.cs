using System;
using System.Collections.Generic;
using System.Text;

using ShopCommon.Data;
using ShopLogic.Interface;

using OSD = ShopCommon.Data.OrderState;
using OSL = ShopLogic.Interface.OrderState;

namespace ShopServerPresentation
{
    internal static class Converters
    {
        public static OSD Convert(OSL i)
        {
            switch (i)
            {
                case OSL.WAITING: return OSD.WAITING;
                case OSL.PREPARED: return OSD.PREPARED;
                case OSL.SENT: return OSD.SENT;
                case OSL.FULFILLED: return OSD.FULFILLED;
                default: throw new InvalidOperationException();
            }
        }

        public static Offer Convert(IOffer i)
        {
            return new Offer
            {
                count = i.count,
                description = i.description,
                id = i.id,
                name = i.name,
                sellPrice = i.sellPrice,
            };
        }

        public static Offer[] Convert(IOffer[] input)
        {
            var output = new Offer[input.Length];

            for (int i = 0; i < output.Length; ++i)
            {
                output[i] = Convert(input[i]);
            }

            return output;
        }

        public static Order Convert(IOrder i)
        {
            return new Order
            {
                creationTime = i.creationTime,
                id = i.id,
                offerChoicesIds = i.offerChoicesIds,
                state = Convert(i.state)
            };
        }

        public static Order[] Convert(IOrder[] input)
        {
            var output = new Order[input.Length];

            for (int i = 0; i < output.Length; ++i)
            {
                output[i] = Convert(input[i]);
            }

            return output;
        }

        public static ShopCart Convert(IShopCart i)
        {
            return new ShopCart
            {
                id = i.id,
                offerChoiceIds = i.offerChoiceIds,
            };
        }

        public static ShopCart[] Convert(IShopCart[] input)
        {
            var output = new ShopCart[input.Length];

            for (int i = 0; i < output.Length; ++i)
            {
                output[i] = Convert(input[i]);
            }

            return output;
        }

        public static OfferChoice Convert(IOfferChoice i)
        {
            return new OfferChoice
            {
                count = i.count,
                id = i.id,
                offerId = i.offerId,
            };
        }

        public static OfferChoice[] Convert(IOfferChoice[] input)
        {
            var output = new OfferChoice[input.Length];

            for (int i = 0; i < output.Length; ++i)
            {
                output[i] = Convert(input[i]);
            }

            return output;
        }
    }
}
