using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

using ShopClientData.Interface;

namespace ShopClientData
{
    public class ClientData : IClientData
    {
        const int default_port = 8081;
        const int default_timeout_ms = 1000;

        HashSet<int> observedTypes = new HashSet<int>();

        AutoResetEvent queueEvent = new AutoResetEvent(false);
        Queue<string> results = new Queue<string>();

        IClientData.OnObserved observedCallback;

        public void RegisterObservedMessageCallback(IClientData.OnObserved callback)
        {
            observedCallback = callback;
        }

        public void RegisterObservedType(int id)
        {
            observedTypes.Add(id);
        }

        public ClientData(int port = default_port)
        {
            Action<string> del = (string s) =>
            {
                if (!IsObserved(s))
                {
                    lock (results)
                    {
                        results.Enqueue(s);
                        queueEvent.Set();
                    }
                }
                else
                {
                    NotifyObservers(s);
                }
            };

            Task.Run(async () => { await WebSocketClient.Connect(new Uri($"ws://localhost:{port}/"), null, del); }).Wait();
        }

        public void Disconnect() { WebSocketClient.CurrentConnection.Disconnect(); }

        public string Interact(string req)
        {
            lock (results)
            {
                if (results.Count != 0) { results.Clear(); }

                queueEvent.Reset();
            }

            var wsc = WebSocketClient.CurrentConnection;

            wsc.SendAsync(req).Wait();

            var withinTimeWindow = queueEvent.WaitOne(default_timeout_ms);

            if (!withinTimeWindow) { throw new Exception("Timeout"); }

            lock (results)
            {
                if (results.Count != 1) { throw new Exception("No result, unexpected"); }

                return results.Dequeue();
            }
        }

        private struct ResponseBase
        {
            public int type { get; set; }
        }

        private bool IsObserved(string message)
        {
            var res = JsonSerializer.Deserialize<ResponseBase>(message);

            return observedTypes.TryGetValue(res.type, out _);
        }

        private void NotifyObservers(string message)
        {
            if (observedCallback != null)
            {
                observedCallback(message);
            }
        }
    }
}
