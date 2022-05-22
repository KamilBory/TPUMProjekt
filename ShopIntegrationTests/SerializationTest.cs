using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;
using System;

using ShopServerPresentation.Types;
using ShopServerPresentation.Calls;
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
            Request<RegisterClientRequest> call = new Request<RegisterClientRequest>
            {
                type = RequestType.REGISTER_CLIENT,
                body = new RegisterClientRequest
                {
                    name = "name",
                    surname = "surname",
                    password = "easypass"
                }
            };

            // serialize call
            var j = JsonSerializer.Serialize(call);

            // deserialize base type
            var dj = JsonSerializer.Deserialize<RequestBase>(j);
            Assert.AreEqual(RequestType.REGISTER_CLIENT, dj.type);

            // deserialize complete type
            var dj2 = JsonSerializer.Deserialize<Request<RegisterClientRequest>>(j);

            Assert.AreEqual(call.type, dj2.type);
            Assert.AreEqual(call.body.name, dj2.body.name);
            Assert.AreEqual(call.body.surname, dj2.body.surname);
            Assert.AreEqual(call.body.password, dj2.body.password);
        }

        public void ClientSelfSerializationExample()
        {

        }
    }
}
