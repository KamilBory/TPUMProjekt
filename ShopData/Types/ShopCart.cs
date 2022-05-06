using System.Collections.Generic;
using ShopData.Interface;

namespace ShopData.Types
{
    public class ShopCart : IShopCart
    {
        public int clientId { get; set; }
        public HashSet<int> offerChoiceIds { get; set; }
    }
}
