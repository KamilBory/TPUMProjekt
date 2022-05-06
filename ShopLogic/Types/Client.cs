using ShopLogic.Interface;

namespace ShopLogic.Types
{
    public class Client : IClient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}
