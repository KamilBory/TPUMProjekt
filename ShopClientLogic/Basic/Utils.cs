using System;
using System.Text.Json;
using SC = ShopCommon;
using SL = ShopClientLogic;

using OSL = ShopClientLogic.Interface.OrderState;
using OSC = ShopCommon.Data.OrderState;

namespace ShopClientLogic.Basic
{
    public static class Utils
    {
        public static T Des<T>(string message) { return JsonSerializer.Deserialize<T>(message); }

        public static string Ser<T>(T o) { return JsonSerializer.Serialize(o); }

        public static OSL Conv(OSC i)
        {
            return i switch
            {
                OSC.WAITING => OSL.WAITING,
                OSC.PREPARED => OSL.PREPARED,
                OSC.SENT => OSL.SENT,
                OSC.FULFILLED => OSL.FULFILLED,
                _ => throw new InvalidOperationException(),
            };
        }

        public static Interface.IOffer Conv(SC.Data.Offer i)
        {
            return new Types.Offer
            {
                count = i.count,
                description = i.description,
                id = i.id,
                name = i.name,
                sellPrice = i.sellPrice,
            };
        }

        public static Interface.IOffer[] Conv(SC.Data.Offer[] input)
        {
            var output = new Interface.IOffer[input.Length];

            for (int i = 0; i < output.Length; ++i)
            {
                output[i] = Conv(input[i]);
            }

            return output;
        }

        public static Interface.IOrder Conv(SC.Data.Order i)
        {
            return new Types.Order
            {
                creationTime = i.creationTime,
                id = i.id,
                offerChoicesIds = i.offerChoicesIds,
                state = Conv(i.state)
            };
        }

        public static Interface.IOrder[] Conv(SC.Data.Order[] input)
        {
            var output = new Interface.IOrder[input.Length];

            for (int i = 0; i < output.Length; ++i)
            {
                output[i] = Conv(input[i]);
            }

            return output;
        }

        public static Interface.IShopCart Conv(SC.Data.ShopCart i)
        {
            return new Types.ShopCart
            {
                id = i.id,
                offerChoiceIds = i.offerChoiceIds,
            };
        }

        public static Interface.IShopCart[] Conv(SC.Data.ShopCart[] input)
        {
            var output = new Interface.IShopCart[input.Length];

            for (int i = 0; i < output.Length; ++i)
            {
                output[i] = Conv(input[i]);
            }

            return output;
        }

        public static Interface.IOfferChoice Conv(SC.Data.OfferChoice i)
        {
            return new Types.OfferChoice
            {
                count = i.count,
                id = i.id,
                offerId = i.offerId,
            };
        }

        public static Interface.IOfferChoice[] Conv(SC.Data.OfferChoice[] input)
        {
            var output = new Interface.IOfferChoice[input.Length];

            for (int i = 0; i < output.Length; ++i)
            {
                output[i] = Conv(input[i]);
            }

            return output;
        }

    }
}
