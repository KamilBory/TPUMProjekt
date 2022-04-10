using System.Collections.Generic;

namespace ShopData.Interface
{
    public struct ShopCart
    {
        public int clientId;
        public HashSet<int> offerChoiceIds;
    }
}
