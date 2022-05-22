using System.Collections.Generic;

namespace ShopClientLogic.Interface
{
    public interface IShopCart
    {
        int id { get; set; }
        int[] offerChoiceIds { get; set; }
    }
}
