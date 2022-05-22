using System;
using System.Text.Json;
using ShopClientData;

namespace ShopClientLogic.Basic
{
    public static class Serialization
    {
        public static T DeserializeCall<T>(string message) where T : AbstractRequest
        {
            return JsonSerializer.Deserialize<T>(message);
        }

        public static T DeserializeResponse<T>(string message) where T : AbstractResponse
        {
            return JsonSerializer.Deserialize<T>(message);
        }

        public static T DeserializeComplexResponse<T>(string message) where T : AbstractResponse
        {
            return JsonSerializer.Deserialize<Response<T>>(message).body;
        }

        public static string Serialize<T>(T o) { return JsonSerializer.Serialize(o); }
    }
}
