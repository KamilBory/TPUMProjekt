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
            int savedId, savedId1, savedId2, savedId3, savedId4;
            var offer = database.CreateOffer();
            offer.name = "Phone"; offer.description = "Phone description"; offer.count = 5; offer.sellPrice = 350;
            savedId = offerRepo.Create(offer);
            offer.name = "Keyboard"; offer.description = "Keyboard description"; offer.count = 7; offer.sellPrice = 150;
            savedId1 = offerRepo.Create(offer);
            offer.name = "Mouse"; offer.description = "Mouse description"; offer.count = 3; offer.sellPrice = 50;
            savedId2 = offerRepo.Create(offer);
            offer.name = "Fridge"; offer.description = "Fridge description"; offer.count = 2; offer.sellPrice = 650;
            savedId3 = offerRepo.Create(offer);
            offer.name = "Monitor"; offer.description = "Monitor description"; offer.count = 6; offer.sellPrice = 250;
            savedId4 = offerRepo.Create(offer);

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
