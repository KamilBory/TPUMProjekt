using ShopData.Interface;

namespace ShopData.Types
{
    public class Client : IClient
    {
        public string name { set; get; }
        public string surname { set; get; }
        public string password { set; get; }
    }
}
