using ShopData.Interface;

namespace ShopData.Types
{
    public class Client : IClient
    {
        public string name { set; get; }
        public string surname { set; get; }
        public string password { set; get; }

        public object Clone()
        {
            return new Client
            {
                name = name.Clone() as string,
                surname = surname.Clone() as string,
                password = password.Clone() as string
            };
        }
    }
}
