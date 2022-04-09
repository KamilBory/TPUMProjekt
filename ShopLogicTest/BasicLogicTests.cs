using Microsoft.VisualStudio.TestTools.UnitTesting;

using Data = ShopData.Interface;
using Impl = ShopLogic.Basic;

using ShopLogic.Interface;
using System;

namespace ShopLogicTest
{
    [TestClass]
    public class BasicLogicTests
    {
        private Data.IDatabase _database;
        private ILogic _logic;

        [TestMethod]
        public void ClientLifetimeHappyPath()
        {
            string testName = "testName";
            string testSurname = "testSurname";
            string testPassword = "testPassword";

            var newClientId = _logic.RegisterClient(testName, testSurname, testPassword);
            var clientLogic = _logic.GetClientLogic(newClientId, testPassword);
            
            {
                var clientData = clientLogic.Get();

                Assert.AreEqual(newClientId, clientData.id);
                Assert.AreEqual(testName, clientData.name);
                Assert.AreEqual(testSurname, clientData.surname);
            }

            {
                string newName = "newName";
                string newSurname = "newSurname";

                clientLogic.Update(new Client { name = newName, surname = newSurname });

                var clientData = clientLogic.Get();

                Assert.AreEqual(newClientId, clientData.id);
                Assert.AreEqual(newName, clientData.name);
                Assert.AreEqual(newSurname, clientData.surname);
            }
        }

        [TestMethod]
        public void ClientWrongId()
        {
            Assert.ThrowsException<Exception>(delegate { _logic.GetClientLogic(0, "testPassword"); }, "Invalid client id");
        }

        [TestMethod]
        public void ClientWrongPassword()
        {
            var clientId = _logic.RegisterClient("name", "surname", "correctPassword");
            Assert.ThrowsException<Exception>(delegate { _logic.GetClientLogic(clientId, "wrongPassword"); }, "Invalid password");
        }

        [TestInitialize]
        public void InitializeTestDatabase()
        {
            _database = new TestDatabase.Database();
            _logic = new Impl.Logic(_database);
        }
    }
}
