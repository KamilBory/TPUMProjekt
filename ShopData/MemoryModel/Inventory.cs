using System;
using ShopData.Interface;

namespace ShopData.MemoryModel
{
    public class Inventory : IInventory
    {
        private int _id;
        private string _name;
        private string _description;
        private int _count;
        private InventorySize _inventorySize;

        public int GetID() { return _id; }

        public string GetName() { return _name; }
        public void SetName(string name) { _name = name; }

        public string GetDescription() { return _description; }
        public void SetDescription(string description) { _description = description; }

        public int GetCount() { return _count; }
        public void SetCount(int count) { _count = count; }

        public InventorySize GetSize() { return _inventorySize; }
        public void SetSize(InventorySize size) { _inventorySize = size; }
    }
}
