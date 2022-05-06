using System.Collections.Generic;

namespace ShopLogic.Interface
{
    public interface IShopCart
    {
        int id { get; set; }
        int[] offerChoiceIds { get; set; }
    }
}
