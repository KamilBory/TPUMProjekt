using ShopLogic.Interface;
using Data = ShopData.Interface;

using System;
using System.Threading;

namespace ShopLogic.Basic
{
    public class Logic : ILogic
    {
        private Data.IDatabase _database;
        private ClientLogic _currentClientLogic;

        private int mangledIndex = -1;
        bool _shuttingDown = false;
        private Thread _updaterThread;
        private AutoResetEvent _updaterSync = new AutoResetEvent(false);

        private readonly int _updaterDelay = 1000;

        public Logic(Data.IDatabase database) : this(database, 1000) { }

        public Logic(Data.IDatabase database, int updaterDelay) { _database = database; _updaterDelay = updaterDelay; }

        public IClientLogic GetClientLogic(int clientId, string password)
        {
            _currentClientLogic = new ClientLogic(clientId, password, _database);

            SpinUpOrderUpdater();

            return _currentClientLogic;
        }

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

        private void SpinUpOrderUpdater()
        {
            var offers = _currentClientLogic.GetAllOffers();

            mangledIndex = offers[new System.Random().Next() % offers.Length].id;

            _updaterThread = new Thread(new ThreadStart(OrderUpdater));

            _updaterThread.Start();
        }

        private void OrderUpdater()
        {
            while (true)
            {
                _updaterSync.WaitOne(_updaterDelay);

                if (_shuttingDown) { break; }

                if (mangledIndex < 0 || _currentClientLogic == null) { continue; }

                var offerRepo = _database.GetOfferRepo();
                var offer = offerRepo.Get(mangledIndex) ?? throw new Exception("Invalid offer id");

                offer.sellPrice += 1;
                offerRepo.Update(mangledIndex, offer);

                var inventoryRepo = _database.GetInventoryRepo();
                var inventory = inventoryRepo.Get(offer.inventoryId) ?? throw new Exception("Invalid inventory id");

                var logicOffer = Utilities.Convert(offer, inventory, mangledIndex);
                _currentClientLogic.UpdateOffer(logicOffer);
            }
        }

        public void Shutdown()
        {
            _shuttingDown = true;
            _updaterSync.Set();
            _updaterThread?.Join();
        }
    }
}
