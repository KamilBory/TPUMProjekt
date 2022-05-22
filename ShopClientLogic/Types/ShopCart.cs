using ShopClientLogic.Interface;

namespace ShopClientLogic.Types
{
    public class ShopCart : IShopCart
    {
        public int id { get; set; }
        public int[] offerChoiceIds { get; set; }
    }
}
