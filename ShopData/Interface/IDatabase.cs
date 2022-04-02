namespace ShopData.Interface
{
    public interface IDatabase
    {
        IRepo<Inventory> GetInventoryRepo();
        IRepo<DeliveryOption> GetDeliveryOptionRepo();
        IRepo<Offer> GetOfferRepo();
        IRepo<OfferChoice> GetOfferChoiceRepo();
        IRepo<Order> GetOrderRepo();
        IRepo<ShopCart> GetShopCartRepo();
    }
}
