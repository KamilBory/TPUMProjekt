using System;
using System.Collections.Generic;
using System.Text;

using Data = ShopData.Interface;
using ShopLogic.Interface;

namespace ShopLogic.Basic
{
    static class Utilities
    {
        public static bool DeliveryOptionAvailableForInventory(Data.DeliveryOption deliveryOption, Data.Inventory inventory)
        {
            var doSize = deliveryOption.maxSize;
            var invSize = inventory.size;

            if (doSize.width <= invSize.width || doSize.height <= invSize.height || doSize.depth <= invSize.depth)
            {
                return false;
            }

            return true;
        }

        public static void Filter(ref KeyValuePair<int, Data.OfferChoice>[] inOut, HashSet<int> ids)
        {
            var filteredValuesList = new List<KeyValuePair<int, Data.OfferChoice>>();

            foreach (var pair in inOut)
            {
                if (!ids.Contains(pair.Key)) { continue; }

                filteredValuesList.Add(pair);
            }

            inOut = filteredValuesList.ToArray();
        }

        public static void Filter(ref KeyValuePair<int, Data.Order>[] inOut, int clientId)
        {
            var filteredValuesList = new List<KeyValuePair<int, Data.Order>>();

            foreach (var pair in inOut)
            {
                if (pair.Value.clientId != clientId) { continue; }

                filteredValuesList.Add(pair);
            }

            inOut = filteredValuesList.ToArray();
        }

        public static void Filter(ref KeyValuePair<int, Data.ShopCart>[] inOut, int clientId)
        {
            var filteredValuesList = new List<KeyValuePair<int, Data.ShopCart>>();

            foreach (var pair in inOut)
            {
                if (pair.Value.clientId != clientId) { continue; }

                filteredValuesList.Add(pair);
            }

            inOut = filteredValuesList.ToArray();
        }

        public static Order.State Convert(Data.OrderState input)
        {
            switch (input)
            {
                case Data.OrderState.WAITING:
                    return Order.State.WAITING;
                case Data.OrderState.PREPARED:
                    return Order.State.PREPARED;
                case Data.OrderState.SENT:
                    return Order.State.SENT;
                case Data.OrderState.FULFILLED:
                    return Order.State.FULFILLED;
            }

            throw new Exception("Input order state enum out of range");
        }

        public static Data.OrderState Convert(Order.State input)
        {
            switch (input)
            {
                case Order.State.WAITING:
                    return Data.OrderState.WAITING;
                case Order.State.PREPARED:
                    return Data.OrderState.PREPARED;
                case Order.State.SENT:
                    return Data.OrderState.SENT;
                case Order.State.FULFILLED:
                    return Data.OrderState.FULFILLED;
            }

            throw new Exception("Input order state enum out of range");
        }

        public static ShopCart.OfferChoice Convert(Data.OfferChoice input, int id) { return new ShopCart.OfferChoice { id = id, offerId = input.offerId, count = input.count }; }
        public static Data.OfferChoice Convert(ShopCart.OfferChoice input) { return new Data.OfferChoice { offerId = input.offerId, count = input.count }; }

        public static ShopCart.OfferChoice[] Convert(KeyValuePair<int, Data.OfferChoice>[] input)
        {
            var output = new ShopCart.OfferChoice[input.Length];
            for (int i = 0; i < input.Length; ++i) { output[i] = Convert(input[i].Value, input[i].Key); }
            return output;
        }

        public static ShopCart.OfferChoice[] Convert(Dictionary<int, Data.OfferChoice> input)
        {
            int i = 0;
            var output = new KeyValuePair<int, Data.OfferChoice>[input.Count];

            foreach (var id in input.Keys) { output[i++] = new KeyValuePair<int, Data.OfferChoice>(id, input[id]); }

            return Convert(output);
        }

        public static Data.OfferChoice[] Convert(ShopCart.OfferChoice[] input)
        {
            var output = new Data.OfferChoice[input.Length];
            for (int i = 0; i < input.Length; ++i) { output[i] = Convert(input[i]); }
            return output;
        }

        public static Order Convert(Data.Order input, int id)
        {
            var output = new Order
            {
                id = id,
                offerChoicesIds = new int[input.offerChoiceIds.Count],
                deliveryOptionId = input.deliveryOptionId,
                state = Convert(input.state)
            };

            input.offerChoiceIds.CopyTo(output.offerChoicesIds);

            return output;
        }

        public static Order[] Convert(KeyValuePair<int, Data.Order>[] input)
        {
            var output = new Order[input.Length];
            for (int i = 0; i < input.Length; ++i) { output[i] = Convert(input[i].Value, input[i].Key); }
            return output;
        }

        public static Data.Client Convert(Client input) { return new Data.Client { name = input.name, surname = input.surname }; }
        public static Client Convert(Data.Client input, int id) { return new Client { id = id, name = input.name, surname = input.surname }; }

        public static DeliveryOption Convert(Data.DeliveryOption input, int id) { return new DeliveryOption { id = id, name = input.name, price = input.price }; }

        public static Offer Convert(Data.Offer offer, Data.Inventory inventory, int id)
        {
            return new Offer
            {
                id = id,
                name = inventory.name,
                description = inventory.description,
                availableCount = inventory.count,
                sellPrice = offer.sellPrice
            };
        }

        public static ShopCart Convert(Data.ShopCart shopCart, int id)
        {
            var offerChoiceIds = new int[shopCart.offerChoiceIds.Count];
            shopCart.offerChoiceIds.CopyTo(offerChoiceIds);

            return new ShopCart
            {
                id = id,
                offerChoiceIds = offerChoiceIds
            };
        }

        public static ShopCart[] Convert(KeyValuePair<int, Data.ShopCart>[] input)
        {
            var output = new ShopCart[input.Length];
            for (int i = 0; i < input.Length; ++i) { output[i] = Convert(input[i].Value, input[i].Key); }
            return output;
        }
    }
}
