using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Interface
{
    public interface IOffer : IIdentifiable
    {
        int GetInventoryID();
        int GetSellPrice();
    }
}
