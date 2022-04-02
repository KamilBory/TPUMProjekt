using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Interface
{
    public interface IInventory : IIdentifiable
    {
        string GetName();
        void SetName(string name);

        string GetDescription();
        void SetDescription(string description);

        int GetCount();
        void SetCount(int count);

        InventorySize GetSize();
        void SetSize(InventorySize size);
    }
}
