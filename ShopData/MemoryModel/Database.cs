using System;
using System.Collections.Generic;
using System.Text;
using ShopData.Interface;

namespace ShopData.MemoryModel
{
    public class Database : IDatabase
    {
        private static int id = 0;
        private static int NewID() { return ++id; }

        private Dictionary<int, Inventory> _dInventory = new Dictionary<int, Inventory>();

        public IInventory GetInventory(int id)
        {
            try { return _dInventory[id]; } catch (KeyNotFoundException) { return null; }
        }

        public bool DeleteInventory(int id) { return _dInventory.Remove(id); }

        public int CreateInventory()
        {
            var id = NewID();
            _dInventory.Add(id, new Inventory());
            return id;
        }
    }
}
