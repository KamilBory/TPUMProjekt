using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;

using LI = ShopLogic.Basic;
using ShopLogic.Interface;

using ShopCommon.Calls;
using ShopCommon.Data;

namespace ShopServerPresentation
{
    public static class Handlers
    {
        private static ILogic logic = new LI.Logic();
        private static Dictionary<WebSocketConnection, IClientLogic> clients = new Dictionary<WebSocketConnection, IClientLogic>();

        public static void ReplaceLogic(ILogic injected) { logic = injected; }

        public static void ShutdownLogic()
        {
            logic.Shutdown();
        }

        public static IClientLogic GetClientContext(WebSocketConnection wsc)
        {
            IClientLogic client;

            if (clients.TryGetValue(wsc, out client))
            {
                return client;
            }

            throw new Exception("No fallback for client context");
        }

        public static IClientLogic GetClientContext(WebSocketConnection wsc, int clientId, string password)
        {
            IClientLogic client;

            if (clients.TryGetValue(wsc, out client))
            {
                return client;
            }
            else
            {
                client = logic.GetClientLogic(clientId, password);

                if (client == null)
                {
                    throw new Exception("Failed to create client context");
                }

                clients.Add(wsc, client);
                return client;
            }

            throw new Exception("No fallback for client context");
        }

        static T ReportReq<T>(T obj) where T : IMessage
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            Console.WriteLine($"[Server] Req: {sf.GetMethod().Name} with: {JsonSerializer.Serialize(obj)}");
            return obj;
        }

        static T ReportRes<T>(T obj) where T : IMessage
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            Console.WriteLine($"[Server] Res: {sf.GetMethod().Name} with: {JsonSerializer.Serialize(obj)}");
            return obj;
        }

        public static RegisterClientResponse RegisterClient(WebSocketConnection wsc, RegisterClientRequest req)
        {
            ReportReq(req);

            var id = logic.RegisterClient(req.name, req.surname, req.password);

            if (id == -1)
            {
                throw new Exception("Failed to register client");
            }

            return ReportRes(new RegisterClientResponse { id = id, });
        }

        public static GetClientResponse GetClient(WebSocketConnection wsc, GetClientRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);
            var client = clientLogic.Get();

            return ReportRes(new GetClientResponse
            {
                id = client.id,
                name = client.name,
                surname = client.surname,
                password = req.password
            });
        }

        public static UpdateClientResponse UpdateClient(WebSocketConnection wsc, UpdateClientRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            IClient client = logic.CreateClient(req.name, req.surname);
            client.id = req.id;

            try
            {
                clientLogic.Update(client);
            }
            catch
            {
                return ReportRes(new UpdateClientResponse { success = false });
            }

            return ReportRes(new UpdateClientResponse { success = true });
        }

        public static GetAllOffersResponse GetAllOffers(WebSocketConnection wsc, GetAllOffersRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var offers = clientLogic.GetAllOffers();

            return ReportRes(new GetAllOffersResponse
            {
                success = true,
                offers = Converters.Convert(offers)
            });
        }

        public static GetAllOrdersResponse GetAllOrders(WebSocketConnection wsc, GetAllOrdersRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var orders = clientLogic.GetAllOrders();

            return ReportRes(new GetAllOrdersResponse
            {
                success = true,
                orders = Converters.Convert(orders)
            });
        }

        public static GetOfferByIdResponse GetOfferById(WebSocketConnection wsc, GetOfferByIdRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var offer = clientLogic.GetOfferById(req.offerId);

            return ReportRes(new GetOfferByIdResponse
            {
                success = true,
                offer = Converters.Convert(offer),
            });
        }

        public static GetOrderByIdResponse GetOrderById(WebSocketConnection wsc, GetOrderByIdRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var order = clientLogic.GetOrderById(req.orderId);

            return ReportRes(new GetOrderByIdResponse
            {
                success = true,
                order = Converters.Convert(order),
            });
        }

        public static GetShopCartByIdResponse GetShopCartById(WebSocketConnection wsc, GetShopCartByIdRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var shopCart = clientLogic.GetShopCartById(req.shopCartId);

            return ReportRes(new GetShopCartByIdResponse
            {
                success = true,
                shopCart = Converters.Convert(shopCart),
            });
        }

        public static GetOfferChoiceByIdResponse GetOfferChoiceById(WebSocketConnection wsc, GetOfferChoiceByIdRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var offerChoice = clientLogic.GetOfferChoiceById(req.choiceId);

            return ReportRes(new GetOfferChoiceByIdResponse
            {
                success = true,
                offerChoice = Converters.Convert(offerChoice),
            });
        }

        public static GetAllShopCartsResponse GetAllShopCarts(WebSocketConnection wsc, GetAllShopCartsRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var shopCarts = clientLogic.GetAllShopCarts();

            return ReportRes(new GetAllShopCartsResponse
            {
                success = true,
                shopCarts = Converters.Convert(shopCarts)
            });
        }

        public static CreateShoppingCartResponse CreateShoppingCart(WebSocketConnection wsc, CreateShoppingCartRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var id = clientLogic.CreateShoppingCart();

            return ReportRes(new CreateShoppingCartResponse
            {
                success = true,
                id = id,
            });
        }

        public static DeleteShoppingCartResponse DeleteShoppingCart(WebSocketConnection wsc, DeleteShoppingCartRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            try
            {
                clientLogic.DeleteShoppingCart(req.shopCartId);
            }
            catch
            {
                return ReportRes(new DeleteShoppingCartResponse { success = false });
            }

            return ReportRes(new DeleteShoppingCartResponse { success = true });
        }

        public static AddOfferToShoppingCartResponse AddOfferToShoppingCart(WebSocketConnection wsc, AddOfferToShoppingCartRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            try
            {
                clientLogic.AddOfferToShoppingCart(req.shopCartId, req.offerId, req.count);
            }
            catch
            {
                return ReportRes(new AddOfferToShoppingCartResponse { success = false });
            }

            return ReportRes(new AddOfferToShoppingCartResponse { success = true });
        }

        public static DeleteOfferFromShoppingCartResponse DeleteOfferFromShoppingCart(WebSocketConnection wsc, DeleteOfferFromShoppingCartRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            try
            {
                clientLogic.DeleteOfferFromShoppingCart(req.shopCartId, req.offerId, req.count);
            }
            catch
            {
                return ReportRes(new DeleteOfferFromShoppingCartResponse { success = false });
            }

            return ReportRes(new DeleteOfferFromShoppingCartResponse { success = true });
        }

        public static CreateOrderFromShoppingCartResponse CreateOrderFromShoppingCart(WebSocketConnection wsc, CreateOrderFromShoppingCartRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var order = clientLogic.CreateOrderFromShoppingCart(req.shopCartId);

            return ReportRes(new CreateOrderFromShoppingCartResponse { order = Converters.Convert(order) });
        }

        public static OfferUpdateSubscriptionResponse SubscribeForOfferUpdate(WebSocketConnection wsc, OfferUpdateSubscriptionRequest req)
        {
            ReportReq(req);

            var clientLogic = GetClientContext(wsc, req.id, req.password);

            var observer = new OfferUpdateObserver(wsc);

            var unsub = clientLogic.SubscribeForOfferUpdate(observer);

            observer.unsubscriber = unsub;

            return ReportRes(new OfferUpdateSubscriptionResponse { success = true });
        }

        // OFFER UPDATE OBSERVER

        public class OfferUpdateObserver : IObserver<IOffer>
        {
            WebSocketConnection wsc;
            public IDisposable unsubscriber;

            public OfferUpdateObserver(WebSocketConnection connection) { wsc = connection; }

            public void OnCompleted()
            {
                unsubscriber?.Dispose();
            }

            public void OnError(Exception error)
            {
                unsubscriber?.Dispose();
            }

            public void OnNext(IOffer value)
            {
                var res = new OfferUpdateNotification
                {
                    offer = Converters.Convert(value)
                };

                try
                {
                    wsc.SendAsync(JsonSerializer.Serialize(res)).Wait();
                }
                catch
                {
                    unsubscriber?.Dispose();
                }
            }
        }
    }
}
