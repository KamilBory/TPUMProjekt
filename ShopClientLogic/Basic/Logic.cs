using System;

using ShopClientData.Interface;
using ShopClientData;

using ShopClientLogic.Interface;
using ShopClientLogic.Types;

namespace ShopClientLogic.Basic
{
    public class Logic : ILogic
    {
        private IClientData clientData;

        public Logic(int port = 8081) { clientData = new ClientData(port); }

        public IClientLogic GetClientLogic(int clientId, string password)
        {
            return new ClientLogic(clientId, password, clientData);
        }

        public int RegisterClient(string name, string surname, string password)
        {
            var req = new Request<RegisterClientCall>
            {
                type = RequestType.REGISTER_CLIENT,
                body = new RegisterClientCall
                {
                    name = name,
                    surname = surname,
                    password = password
                },
            };

            var resStr = clientData.Interact(Serialization.Serialize(req));
            var res = Serialization.DeserializeResponse<RegisterClientResponse>(resStr);

            return res.id;
        }

        public void Shutdown() {
            //clientData.Interact("SHUTDOWN");
        }

        // type creation for layers above

        public IClient CreateClient() { return new Client(); }
        public IOffer CreateOffer() { return new Offer(); }
        public IOfferChoice CreateOfferChoice() { return new OfferChoice(); }
        public IOrder CreateOrder() { return new Order(); }
        public IShopCart CreateShopCart() { return new ShopCart(); }

        public IClient CreateClient(string name, string surname)
        {
            return new Client { name = name, surname = surname };
        }

        public IOffer CreateOffer(int sellPrice, string name, string description, int count)
        {
            return new Offer { sellPrice = sellPrice, name = name, description = description, count = count };
        }

        public IOfferChoice CreateOfferChoice(int offerId, int count)
        {
            return new OfferChoice { offerId = offerId, count = count };
        }

        public IOrder CreateOrder(int[] offerChoicesIds, DateTime creationTime, OrderState state)
        {
            return new Order { offerChoicesIds = offerChoicesIds, creationTime = creationTime, state = state };
        }

        public IShopCart CreateShopCart(int[] offerChoiceIds)
        {
            return new ShopCart { offerChoiceIds = offerChoiceIds };
        }
    }
}
