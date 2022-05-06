using ShopLogic.Interface;

namespace ShopLogic.Types
{
    public class ShopCart : IShopCart
    {
        public int id { get; set; }
        public int[] offerChoiceIds { get; set; }
    }
}
