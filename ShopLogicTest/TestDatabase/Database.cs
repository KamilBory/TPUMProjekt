using ShopData.Interface;
using System.Collections.Generic;

namespace ShopLogicTest.TestDatabase
{
    public class Database : IDatabase
    {
        private Repo<Client> _clientRepo = new Repo<Client>();
        private Repo<Inventory> _inventoryRepo = new Repo<Inventory>();
        private Repo<DeliveryOption> _deliveryOptionRepo = new Repo<DeliveryOption>();
        private Repo<Offer> _offerRepo = new Repo<Offer>();
        private Repo<OfferChoice> _offerChoiceRepo = new Repo<OfferChoice>();
        private Repo<Order> _orderRepo = new Repo<Order>();
        private Repo<ShopCart> _shopCartRepo = new Repo<ShopCart>();

        public IRepo<Client> GetClientRepo() { return _clientRepo; }
        public IRepo<Inventory> GetInventoryRepo() { return _inventoryRepo; }
        public IRepo<DeliveryOption> GetDeliveryOptionRepo() { return _deliveryOptionRepo; }
        public IRepo<Offer> GetOfferRepo() { return _offerRepo; }
        public IRepo<OfferChoice> GetOfferChoiceRepo() { return _offerChoiceRepo; }
        public IRepo<Order> GetOrderRepo() { return _orderRepo; }
        public IRepo<ShopCart> GetShopCartRepo() { return _shopCartRepo; }

        public void SetClientRepo(Dictionary<int, Client> dict) { _clientRepo.Init(dict); }
        public void SetInventoryRepo(Dictionary<int, Inventory> dict) { _inventoryRepo.Init(dict); }
        public void SetDeliveryOptionRepo(Dictionary<int, DeliveryOption> dict) { _deliveryOptionRepo.Init(dict); }
        public void SetOfferRepo(Dictionary<int, Offer> dict) { _offerRepo.Init(dict); }
        public void SetOfferChoiceRepo(Dictionary<int, OfferChoice> dict) { _offerChoiceRepo.Init(dict); }
        public void SetOrderRepo(Dictionary<int, Order> dict) { _orderRepo.Init(dict); }
        public void SetShopCartRepo(Dictionary<int, ShopCart> dict) { _shopCartRepo.Init(dict); }
    }
}
