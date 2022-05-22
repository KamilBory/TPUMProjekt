using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;
using System;

using SSP = ShopServerPresentation;
using SSC = ShopClientData;
using System.Collections.Generic;

namespace ShopIntegrationTests
{
    [TestClass]
    public class ConnectionTest
    {
        SSP.WebSocketConnection wsServer;
        System.Threading.AutoResetEvent serverReady;
        Task serverTask;

        SSC.WebSocketConnection wsClient;

        Queue<string> serverQueue;
        System.Threading.AutoResetEvent serverEvent;

        Queue<string> clientQueue;
        System.Threading.AutoResetEvent clientEvent;

        [TestMethod]
        public void ServerClientCommunication()
        {
            string example = "message";

            serverReady.WaitOne();
            wsServer.SendAsync(example).Wait();

            clientEvent.WaitOne();
            Assert.IsTrue(clientQueue.Count == 1);

            var recv = clientQueue.Dequeue();
            Assert.IsNotNull(recv);

            Assert.AreEqual(example, recv);
        }

        [TestMethod]
        public void ClientServerCommunication()
        {
            string example = "message";

            serverReady.WaitOne();
            wsServer.onMessage = (s) => { serverQueue.Enqueue(s); serverEvent.Set(); };

            wsClient.SendAsync(example).Wait();

            serverEvent.WaitOne();
            Assert.IsTrue(serverQueue.Count == 1);

            var recv = serverQueue.Dequeue();
            Assert.IsNotNull(recv);

            Assert.AreEqual(example, recv);
        }

        static int portNum = 8080;

        [TestInitialize]
        public void Setup()
        {
            clientEvent = new System.Threading.AutoResetEvent(false);
            serverEvent = new System.Threading.AutoResetEvent(false);
            serverReady = new System.Threading.AutoResetEvent(false);

            clientQueue = new Queue<string>();
            serverQueue = new Queue<string>();

            var task1 = Task.Run(async () => { await SSP.Server.Run(portNum, (SSP.WebSocketConnection wsc) => { wsServer = wsc; serverReady.Set(); }); });
            var task2 = Task.Run(async () => { await SSC.WebSocketClient.Connect(new Uri($"ws://localhost:{portNum}/"), null, (string s) => { clientQueue.Enqueue(s); clientEvent.Set(); }); });

            serverTask = task1;
            task2.Wait();

            wsClient = SSC.WebSocketClient.CurrentConnection;
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
            wsClient.Disconnect().Wait();
            wsServer.Disconnect().Wait();

            SSP.Server.cts.Cancel();
            PokeServer();
            serverTask.Wait();

            ++portNum;
        }
    }
}
