using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Interface
{
    public interface IDatabase
    {
        IInventory GetInventory(int id);
        bool DeleteInventory(int id);
        int CreateInventory();
    }
}
