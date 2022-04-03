using ShopData.Interface;

namespace ShopData.MemoryModel
{
    public class Database : IDatabase
    {
        private Repo<Inventory> _inventoryRepo = new Repo<Inventory>();
        private Repo<DeliveryOption> _deliveryOptionRepo = new Repo<DeliveryOption>();
        private Repo<Offer> _offerRepo = new Repo<Offer>();
        private Repo<OfferChoice> _offerChoiceRepo = new Repo<OfferChoice>();
        private Repo<Order> _orderRepo = new Repo<Order>();
        private Repo<ShopCart> _shopCartRepo = new Repo<ShopCart>();

        public IRepo<Inventory> GetInventoryRepo() { return _inventoryRepo; }
        public IRepo<DeliveryOption> GetDeliveryOptionRepo() { return _deliveryOptionRepo; }
        public IRepo<Offer> GetOfferRepo() { return _offerRepo; }
        public IRepo<OfferChoice> GetOfferChoiceRepo() { return _offerChoiceRepo; }
        public IRepo<Order> GetOrderRepo() { return _orderRepo; }
        public IRepo<ShopCart> GetShopCartRepo() { return _shopCartRepo; }
    }
}
