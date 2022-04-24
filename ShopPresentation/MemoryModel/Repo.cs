using System.Collections.Generic;
using ShopData.Interface;

namespace ShopPresentation.MemoryModel
{
    class Repo<T> : IRepo<T> where T : struct
    {
        private static int id = 0;
        private static int NewID() { return ++id; }

        private Dictionary<int, T> _dInventory = new Dictionary<int, T>();

        public T? Get(int id)
        {
            try { return _dInventory[id]; } catch (KeyNotFoundException) { return null; }
        }

        public bool Delete(int id)
        {
            return _dInventory.Remove(id);
        }

        public bool Update(int id, T obj)
        {
            try
            {
                _dInventory[id] = obj;
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public int Create(T obj)
        {
            var id = NewID();
            _dInventory.Add(id, obj);
            return id;
        }

        public int[] ListIds()
        {
            int i = 0;
            var ids = new int[_dInventory.Count];

            foreach (var key in _dInventory.Keys) { ids[i++] = key; }

            return ids;
        }

        public KeyValuePair<int, T>[] List()
        {
            int i = 0;
            var ivs = new KeyValuePair<int, T>[_dInventory.Count];

            foreach (var id in ListIds()) { ivs[i++] = new KeyValuePair<int, T>(id, _dInventory[id]); }

            return ivs;
        }
    }
}
