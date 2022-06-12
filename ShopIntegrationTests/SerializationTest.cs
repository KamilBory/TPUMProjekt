using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;

using ShopCommon.Data;
using ShopCommon.Calls;
using ShopServerPresentation;

using System.Text.Json;

namespace ShopIntegrationTests
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void ServerSelfSerializationExample()
        {
            RegisterClientRequest call = new RegisterClientRequest
            {
                    name = "name",
                    surname = "surname",
                    password = "easypass"
            };

            // serialize call
            var j = JsonSerializer.Serialize(call);

            // deserialize base type
            var dj = JsonSerializer.Deserialize<MessageBase>(j);
            Assert.AreEqual(Type.REGISTER_CLIENT, dj.type);

            // deserialize complete type
            var dj2 = JsonSerializer.Deserialize<RegisterClientRequest>(j);

            Assert.AreEqual(call.type, dj2.type);
            Assert.AreEqual(call.name, dj2.name);
            Assert.AreEqual(call.surname, dj2.surname);
            Assert.AreEqual(call.password, dj2.password);
        }

        public void ClientSelfSerializationExample()
        {

        }
    }
}
