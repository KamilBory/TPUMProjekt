using System;
using System.Threading;

using ShopLogic.Interface;

namespace ShopServerPresentation
{
    public class Program
    {
        public static WebSocketConnection connection;

        static void Main(string[] args)
        {
            Console.WriteLine("[Server] Starting at port 8081");
            Server.Run(8081, ConnectionHandler).Wait();
        }

        public static void ConnectionHandler(WebSocketConnection wsc)
        {
            connection = wsc;
            connection.onMessage = (message) => { OnMessage(connection, message); };
            connection.onError = () => { OnError(connection); };
            connection.onClose = () => { OnClose(connection); };

            Console.WriteLine($"[Server] Started");
        }

        static async void OnMessage(WebSocketConnection wsc, string message)
        {
            Console.WriteLine($"[Server] Received: {message}");

            if (message == "SHUTDOWN")
            {
                Handlers.ShutdownLogic();
                return;
            }

            var res = Serialization.ContextCall(wsc, message);

            Console.WriteLine($"[Server] Responding: {res}");

            await wsc.SendAsync(res);
        }

        static void OnClose(WebSocketConnection _)
        {
            Console.WriteLine("[Server] Server close");
        }

        static void OnError(WebSocketConnection _)
        {
            Console.WriteLine("[Server] Server error");
        }
    }
}
