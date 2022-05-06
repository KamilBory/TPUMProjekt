using System;
using System.Collections.Generic;

using Data = ShopData.Interface;
using ShopLogic.Types;
using ShopLogic.Interface;

namespace ShopLogic.Basic
{
    static class Utilities
    {
        public static void Filter(ref KeyValuePair<int, Data.IOfferChoice>[] inOut, HashSet<int> ids)
        {
            var filteredValuesList = new List<KeyValuePair<int, Data.IOfferChoice>>();

            foreach (var pair in inOut)
            {
                if (!ids.Contains(pair.Key)) { continue; }

                filteredValuesList.Add(pair);
            }

            inOut = filteredValuesList.ToArray();
        }

        public static void Filter(ref KeyValuePair<int, Data.IOrder>[] inOut, int clientId)
        {
            var filteredValuesList = new List<KeyValuePair<int, Data.IOrder>>();

            foreach (var pair in inOut)
            {
                if (pair.Value.clientId != clientId) { continue; }

                filteredValuesList.Add(pair);
            }

            inOut = filteredValuesList.ToArray();
        }

        public static void Filter(ref KeyValuePair<int, Data.IShopCart>[] inOut, int clientId)
        {
            var filteredValuesList = new List<KeyValuePair<int, Data.IShopCart>>();

            foreach (var pair in inOut)
            {
                if (pair.Value.clientId != clientId) { continue; }

                filteredValuesList.Add(pair);
            }

            inOut = filteredValuesList.ToArray();
        }

        public static OrderState Convert(Data.OrderState input)
        {
            switch (input)
            {
                case Data.OrderState.WAITING:
                    return OrderState.WAITING;
                case Data.OrderState.PREPARED:
                    return OrderState.PREPARED;
                case Data.OrderState.SENT:
                    return OrderState.SENT;
                case Data.OrderState.FULFILLED:
                    return OrderState.FULFILLED;
            }

            throw new Exception("Input order state enum out of range");
        }

        public static Data.OrderState Convert(OrderState input)
        {
            switch (input)
            {
                case OrderState.WAITING:
                    return Data.OrderState.WAITING;
                case OrderState.PREPARED:
                    return Data.OrderState.PREPARED;
                case OrderState.SENT:
                    return Data.OrderState.SENT;
                case OrderState.FULFILLED:
                    return Data.OrderState.FULFILLED;
            }

            throw new Exception("Input order state enum out of range");
        }

        public static OfferChoice Convert(Data.IOfferChoice input, int id)
        {
            return new OfferChoice { id = id, offerId = input.offerId, count = input.count };
        }

        public static Data.IOfferChoice Convert(IOfferChoice input, Data.IDatabase database)
        {
            return database.CreateOfferChoice(input.offerId, input.count);
        }

        public static OfferChoice[] Convert(KeyValuePair<int, Data.IOfferChoice>[] input)
        {
            var output = new OfferChoice[input.Length];
            for (int i = 0; i < input.Length; ++i) { output[i] = Convert(input[i].Value, input[i].Key); }
            return output;
        }

        public static OfferChoice[] Convert(Dictionary<int, Data.IOfferChoice> input, Data.IDatabase database)
        {
            int i = 0;
            var output = new KeyValuePair<int, Data.IOfferChoice>[input.Count];

            foreach (var id in input.Keys) { output[i++] = new KeyValuePair<int, Data.IOfferChoice>(id, input[id]); }

            return Convert(output);
        }

        public static Data.IOfferChoice[] Convert(IOfferChoice[] input, Data.IDatabase database)
        {
            var output = new Data.IOfferChoice[input.Length];
            for (int i = 0; i < input.Length; ++i) { output[i] = Convert(input[i], database); }
            return output;
        }

        public static Order Convert(Data.IOrder input, int id)
        {
            var output = new Order
            {
                id = id,
                offerChoicesIds = new int[input.offerChoiceIds.Count],
                state = Convert(input.state)
            };

            input.offerChoiceIds.CopyTo(output.offerChoicesIds);

            return output;
        }

        public static Order[] Convert(KeyValuePair<int, Data.IOrder>[] input)
        {
            var output = new Order[input.Length];
            for (int i = 0; i < input.Length; ++i) { output[i] = Convert(input[i].Value, input[i].Key); }
            return output;
        }

        public static Data.IClient Convert(IClient input, string password, Data.IDatabase database)
        {
            return database.CreateClient(input.name, input.surname, password);
        }

        public static Client Convert(Data.IClient input, int id)
        {
            return new Client { id = id, name = input.name, surname = input.surname };
        }

        public static Offer Convert(Data.IOffer offer, int id)
        {
            return new Offer
            {
                id = id,
                name = offer.name,
                description = offer.description,
                count = offer.count,
                sellPrice = offer.sellPrice
            };
        }

        public static ShopCart Convert(Data.IShopCart shopCart, int id)
        {
            var offerChoiceIds = new int[shopCart.offerChoiceIds.Count];
            shopCart.offerChoiceIds.CopyTo(offerChoiceIds);

            return new ShopCart
            {
                id = id,
                offerChoiceIds = offerChoiceIds
            };
        }

        public static ShopCart[] Convert(KeyValuePair<int, Data.IShopCart>[] input)
        {
            var output = new ShopCart[input.Length];
            for (int i = 0; i < input.Length; ++i) { output[i] = Convert(input[i].Value, input[i].Key); }
            return output;
        }
    }
}
