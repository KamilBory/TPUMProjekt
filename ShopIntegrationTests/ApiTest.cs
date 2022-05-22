using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using SSP = ShopServerPresentation;
using SSC = ShopClientData;

namespace ShopIntegrationTests
{
    [TestClass]
    public class ApiTest
    {
        Task serverTask;
        Task clientTask;

        SSP.WebSocketConnection wss;
        SSC.WebSocketConnection wsc;

        System.Threading.AutoResetEvent serverReady;
        System.Threading.AutoResetEvent clientReady;

        Queue<string> clientQueue;
        System.Threading.AutoResetEvent clientEvent;

        static int portNum = 8090;

        public void SendAsync<T>(T obj)
        {
            wsc.SendAsync(JsonSerializer.Serialize(obj)).Wait();
        }

        public T GetResponse<T>() where T : SSP.Calls.AbstractResponse
        {
            clientEvent.WaitOne();
            Assert.AreEqual(clientQueue.Count, 1);
            return SSP.Serialization.DeserializeResponse<T>(clientQueue.Dequeue());
        }

        [TestMethod]
        public void RegisterClient()
        {
            SendAsync(new SSP.Calls.Request<SSP.Calls.RegisterClientRequest>
            {
                type = SSP.Calls.RequestType.REGISTER_CLIENT,
                body = new SSP.Calls.RegisterClientRequest
                {
                    name = "name",
                    surname = "surname",
                    password = "password",
                }
            });

            var res = GetResponse<SSP.Calls.RegisterClientResponse>();

            Assert.AreEqual(res.id, 1);
        }

        [TestInitialize]
        public void setup()
        {
            clientEvent = new System.Threading.AutoResetEvent(false);
            serverReady = new System.Threading.AutoResetEvent(false);
            clientReady = new System.Threading.AutoResetEvent(false);

            clientQueue = new Queue<string>();

            serverTask = Task.Run(async () => { await SSP.Server.Run(portNum, (wsc) => { SSP.Program.ConnectionHandler(wsc); wss = wsc; }); });
            clientTask = Task.Run(async () => { await SSC.WebSocketClient.Connect(new Uri($"ws://localhost:{portNum}/"), null, (string s) => { clientQueue.Enqueue(s); clientEvent.Set(); }); });

            clientTask.Wait();

            wsc = SSC.WebSocketClient.CurrentConnection;
        }

        public void PokeServer()
        {
            try
            {
                Task.Run(async () =>
                {
                    await SSC.WebSocketClient.Connect(new Uri($"ws://localhost:{portNum}/"), null, null);
                }).Wait();
            }
            catch
            {
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            wss?.Disconnect().Wait();
            if (wsc != null && wsc.IsConnected) { wsc.Disconnect().Wait(); }

            SSP.Server.cts.Cancel();
            PokeServer();
            serverTask.Wait();

            ++portNum;
        }
    }
}
