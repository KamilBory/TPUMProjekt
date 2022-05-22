using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopClientData
{
    public static class WebSocketClient
    {
        public delegate void OnMessage(string data);

        public static WebSocketConnection CurrentConnection { get; private set; }

        public static async Task<WebSocketConnection> Connect(Uri peer, Action<string> log, Action<string> onMessage)
        {
            ClientWebSocket clientWebSocket = new ClientWebSocket();
            await clientWebSocket.ConnectAsync(peer, CancellationToken.None);

            switch (clientWebSocket.State)
            {
                case WebSocketState.Open:
                    log?.Invoke($"Opened connection to remote server: {peer}");

                    WebSocketConnection socketConnection = new ClientWebSocketConnection(clientWebSocket, peer, log);

                    socketConnection.onMessage = onMessage;
                    CurrentConnection = socketConnection;

                    return socketConnection;

                default:
                    string errorMessage = $"Cannot connect to remote server {peer} with status {clientWebSocket.State}";
                    log?.Invoke(errorMessage);
                    throw new WebSocketException(errorMessage);
            }
        }

        public static async Task Disconnect()
        {
            await CurrentConnection.Disconnect();
            CurrentConnection = null;
        }

        #region clientwsConnection
        private class ClientWebSocketConnection : WebSocketConnection
        {
            private ClientWebSocket clientWebSocket = null;
            private Uri peer = null;
            private readonly Action<string> log;

            public ClientWebSocketConnection(ClientWebSocket clientWebSocket, Uri peer, Action<string> log)
            {
                this.log = log;
                this.peer = peer;
                this.clientWebSocket = clientWebSocket;
                Task.Factory.StartNew(ClientMessageLoop);
            }

            public override bool IsConnected => clientWebSocket.State == WebSocketState.Open;

            protected override Task SendTask(string message)
            {
                return clientWebSocket.SendAsync(message.UseArraysSegment(), WebSocketMessageType.Text, true, CancellationToken.None); ;
            }

            public override Task Disconnect()
            {
                return clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Shutdown procedure started", CancellationToken.None);
            }

            private void ClientMessageLoop()
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        ArraySegment<byte> segment = new ArraySegment<byte>(buffer);
                        WebSocketReceiveResult result = clientWebSocket.ReceiveAsync(segment, CancellationToken.None).Result;
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            onClose?.Invoke();
                            clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "I am closing", CancellationToken.None).Wait();
                            return;
                        }
                        int count = result.Count;
                        while (!result.EndOfMessage)
                        {
                            if (count >= buffer.Length)
                            {
                                onClose?.Invoke();
                                clientWebSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None).Wait();
                                return;
                            }
                            segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                            result = clientWebSocket.ReceiveAsync(segment, CancellationToken.None).Result;
                            count += result.Count;
                        }
                        string message = Encoding.UTF8.GetString(buffer, 0, count);
                        onMessage?.Invoke(message);
                    }
                }
                catch (Exception _ex)
                {
                    log($"Connection has been broken because of an exception {_ex}");
                    clientWebSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Connection has been broken because of an exception", CancellationToken.None).Wait();
                }
            }
        }

        #endregion
        internal static ArraySegment<byte> UseArraysSegment(this string message)
        {
            byte[] bytesBuffer = Encoding.UTF8.GetBytes(message);
            return new ArraySegment<byte>(bytesBuffer);
        }
    }
}
