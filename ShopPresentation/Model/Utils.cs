using System.Collections.ObjectModel;

using Logic = ShopLogic.Interface;

namespace ShopPresentation.Model
{
    static internal class Utils
    {
        public static Offer ToOffer(Logic.IOffer offer)
        {
            return new Offer
            {
                id = offer.id,
                name = offer.name,
                description = offer.description,
                price = offer.sellPrice,
            };
        }

        public static Order ToOrder(Logic.IOrder iorder, Logic.IClientLogic clientLogic)
        {
            var order = new Order { entries = new ObservableCollection<Order.Entry>() };
            order.id = iorder.id;

            for (int i = 0; i < iorder.offerChoicesIds.Length; ++i)
            {
                var entry = new Order.Entry();
                entry.choiceId = iorder.offerChoicesIds[i];

                var offerChoice = clientLogic.GetOfferChoiceById(entry.choiceId);
                entry.count = offerChoice.count;
                entry.offerId = offerChoice.offerId;

                var offer = clientLogic.GetOfferById(entry.offerId);
                entry.name = offer.name.Clone() as string;
                entry.price = offer.sellPrice;
                entry.sumPrice = entry.price * entry.count;

                order.entries.Add(entry);
            }

            return order;
        }

        public static Cart ToCart(Logic.IShopCart icart, Logic.IClientLogic clientLogic)
        {
            Cart cart = new Cart { entries = new ObservableCollection<Cart.Entry>() };
            cart.id = icart.id;

            for (int i = 0; i < icart.offerChoiceIds.Length; ++i)
            {
                var entry = new Cart.Entry();
                entry.choiceId = icart.offerChoiceIds[i];
                entry.parentCart = cart.id;

                var offerChoice = clientLogic.GetOfferChoiceById(entry.choiceId);
                entry.count = offerChoice.count;
                entry.offerId = offerChoice.offerId;

                var offer = clientLogic.GetOfferById(entry.offerId);
                entry.name = offer.name.Clone() as string;
                entry.price = offer.sellPrice;
                entry.sumPrice = entry.price * entry.count;

                cart.entries.Add(entry);
            }

            return cart;
        }

        static public Offer[] ConvertArray(Logic.IOffer[] offers)
        {
            Offer[] offerArray = new Offer[offers.Length];

            for (int i = 0; i < offers.Length; ++i)
            {
                offerArray[i] = ToOffer(offers[i]);
            }

            return offerArray;
        }

        static public Order[] ConvertArray(Logic.IOrder[] orders, Logic.IClientLogic clientLogic)
        {
            Order[] orderArray = new Order[orders.Length];

            for (int i = 0; i < orders.Length; ++i)
            {
                orderArray[i] = ToOrder(orders[i], clientLogic);
            }

            return orderArray;
        }

        static public Cart[] ConvertArray(Logic.IShopCart[] carts, Logic.IClientLogic clientLogic)
        {
            Cart[] cartArray = new Cart[carts.Length];

            for (int i = 0; i < carts.Length; ++i)
            {
                cartArray[i] = ToCart(carts[i], clientLogic);
            }

            return cartArray;
        }
    }
}
