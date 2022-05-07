using ShopLogic.Interface;
using ShopLogic.Types;

using Data = ShopData.Interface;
using DataImpl = ShopData.MemoryModel;

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

        static private Data.IDatabase CreateDefaultDatabase()
        {
            var database = new DataImpl.Database();
            // ***********************************************************
            // * PLEASE INITIALIZE THE WHOOOOLE DATABASE HERE PLEEEASSSS *
            // ***********************************************************
            var offerRepo = database.GetOfferRepo();
            offerRepo.Create(database.CreateOffer(350, "Phone", "Phone description", 5));
            offerRepo.Create(database.CreateOffer(50, "Keyboard", "Keyboard description", 2));
            offerRepo.Create(database.CreateOffer(250, "Monitor", "Monitor description", 6));
            offerRepo.Create(database.CreateOffer(100, "Mouse", "Mouse description", 9));
            offerRepo.Create(database.CreateOffer(600, "Fridge", "Fridge description", 2));

            return database;
        }

        public Logic() : this(CreateDefaultDatabase()) { }

        public Logic(Data.IDatabase database) : this(database, 1000) { }

        public Logic(int updaterDelay) : this(CreateDefaultDatabase(), updaterDelay) { }

        public Logic(Data.IDatabase database, int updaterDelay) { _database = database; _updaterDelay = updaterDelay; }

        public IClientLogic GetClientLogic(int clientId, string password)
        {
            _currentClientLogic = new ClientLogic(clientId, password, _database);

            SpinUpOrderUpdater();

            return _currentClientLogic;
        }

        public int RegisterClient(string name, string surname, string password)
        {
            var newClient = _database.CreateClient(name, surname, password);

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
                if (_updaterDelay > 0)
                {
                    _updaterSync.WaitOne(_updaterDelay);
                }

                if (_shuttingDown) { break; }

                if (mangledIndex < 0 || _currentClientLogic == null) { continue; }

                var offerRepo = _database.GetOfferRepo();
                var offer = offerRepo.Get(mangledIndex) ?? throw new Exception("Invalid offer id");

                offer.sellPrice += 1;
                offerRepo.Update(mangledIndex, offer);

                var logicOffer = Utilities.Convert(offer, mangledIndex);
                _currentClientLogic.UpdateOffer(logicOffer);
            }
        }

        public void Shutdown()
        {
            _shuttingDown = true;
            _updaterSync.Set();
            _updaterThread?.Join();
        }

        // type creation for layers above

        public IClient CreateClient() { return new Client(); }
        public IOffer CreateOffer() { return new Offer(); }
        public IOfferChoice CreateOfferChoice() { return new OfferChoice(); }
        public IOrder CreateOrder() { return new Order(); }
        public IShopCart CreateShopCart() { return new ShopCart(); }

        public IClient CreateClient(string name, string surname)
        {
            return new Client { name = name, surname = surname };
        }

        public IOffer CreateOffer(int sellPrice, string name, string description, int count)
        {
            return new Offer { sellPrice = sellPrice, name = name, description = description, count = count };
        }

        public IOfferChoice CreateOfferChoice(int offerId, int count)
        {
            return new OfferChoice { offerId = offerId, count = count };
        }

        public IOrder CreateOrder(int[] offerChoicesIds, DateTime creationTime, OrderState state)
        {
            return new Order { offerChoicesIds = offerChoicesIds, creationTime = creationTime, state = state };
        }

        public IShopCart CreateShopCart(int[] offerChoiceIds)
        {
            return new ShopCart { offerChoiceIds = offerChoiceIds };
        }
    }
}
