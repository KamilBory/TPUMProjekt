using ShopLogic.Interface;
using Data = ShopData.Interface;

namespace ShopLogic.Basic
{
    public class Logic : ILogic
    {
        private Data.IDatabase _database;

        public Logic(Data.IDatabase database) { _database = database; }

        public IClientLogic GetClientLogic(int clientId, string password) { return new ClientLogic(clientId, password, _database); }

        public int RegisterClient(string name, string surname, string password)
        {
            var newClient = new Data.Client
            {
                name = name,
                surname = surname,
                password = password
            };

            var clientRepo = _database.GetClientRepo();

            try
            {
                return clientRepo.Create(newClient);
            }
            catch
            {
                // TODO throw
                return -1;
            }
        }
    }
}
