using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

using ShopCommon.Calls;

namespace ShopCommonTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void SerializationTest()
        {
            GetAllOffersRequest request = new GetAllOffersRequest { id = 420, password = "password" };

            Assert.AreEqual(Type.GET_ALL_OFFERS, request.type);

            var doc = JsonSerializer.Serialize(request);
            System.Console.WriteLine(doc);

            var requestBse = JsonSerializer.Deserialize<MessageBase>(doc);
            var requestRet = JsonSerializer.Deserialize<GetAllOffersRequest>(doc);

            Assert.IsNotNull(requestBse);
            Assert.AreEqual(request.type, requestBse.type);

            Assert.IsNotNull(requestRet);
            Assert.AreEqual(request.type, requestRet.type);
            Assert.AreEqual(request.id, requestRet.id);
            Assert.AreEqual(request.password, requestRet.password);
        }
    }
}
