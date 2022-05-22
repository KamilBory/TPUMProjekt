using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ShopServerPresentation
{
    public static class Server
    {
        public static CancellationTokenSource cts;
        public static CancellationToken ct;

        public static async Task Run(int p2pPort, Action<WebSocketConnection> onConnection)
        {
            Uri uri = new Uri($@"http://localhost:{p2pPort}/");
            await Loop(uri, onConnection);
        }

        public static async Task Loop(Uri uri, Action<WebSocketConnection> onConnection)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(uri.ToString());
            listener.Start();

            cts = new CancellationTokenSource();
            ct = cts.Token;

            await Task.Run(async () =>
            {
                while (true)
                {
                    HttpListenerContext httpListenerContext = await listener.GetContextAsync();

                    if (ct.IsCancellationRequested)
                    {
                        httpListenerContext.Response.StatusCode = 400;
                        httpListenerContext.Response.Close();
                        return;
                    }

                    if (!httpListenerContext.Request.IsWebSocketRequest)
                    {
                        httpListenerContext.Response.StatusCode = 400;
                        httpListenerContext.Response.Close();
                    }

                    HttpListenerWebSocketContext context = await httpListenerContext.AcceptWebSocketAsync(null);
                    WebSocketConnection connection = new ServerWebSocketConnection(context.WebSocket, httpListenerContext.Request.RemoteEndPoint);
                    onConnection?.Invoke(connection);
                }
            }, ct);
        }

        private class ServerWebSocketConnection : WebSocketConnection
        {
            public ServerWebSocketConnection(WebSocket webSocket, IPEndPoint remoteEndPoint)
            {
                this.webSocket = webSocket;
                this.remoteEndPoint = remoteEndPoint;
                Task.Factory.StartNew(() => ServerMessageLoop(webSocket));
            }

            protected override Task SendTask(string message)
            {
                return webSocket.SendAsync(message.GetArraySegment(), WebSocketMessageType.Text, true, CancellationToken.None);
            }

            public override Task Disconnect()
            {
                return webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing started", CancellationToken.None);
            }

            public override string ToString()
            {
                return remoteEndPoint.ToString();
            }

            private WebSocket webSocket = null;
            private IPEndPoint remoteEndPoint;

            private void ServerMessageLoop(WebSocket webSocket)
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    ArraySegment<byte> segments = new ArraySegment<byte>(buffer);
                    WebSocketReceiveResult receiveResult = webSocket.ReceiveAsync(segments, CancellationToken.None).Result;

                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        onClose?.Invoke();

                        webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);

                        return;
                    }

                    int count = receiveResult.Count;

                    while (!receiveResult.EndOfMessage)
                    {
                        if (count >= buffer.Length)
                        {
                            onClose?.Invoke();

                            webSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "Waiting too long", CancellationToken.None);

                            return;
                        }

                        segments = new ArraySegment<byte>(buffer, count, buffer.Length - count);

                        receiveResult = webSocket.ReceiveAsync(segments, CancellationToken.None).Result;

                        count += receiveResult.Count;
                    }

                    string _message = Encoding.UTF8.GetString(buffer, 0, count);

                    onMessage?.Invoke(_message);
                }
            }
        }

        internal static ArraySegment<byte> GetArraySegment(this string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            return new ArraySegment<byte>(buffer);
        }
    }
}
