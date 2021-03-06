using System.Collections.Generic;
using System;
using ShopData.Interface;

namespace ShopData.MemoryModel
{
    class Repo<T> : IRepo<T> where T : class, ICloneable
    {
        private static int id = 0;
        private static object idLock = new object();

        private static int NewID()
        {
            lock (idLock)
            {
                return ++id;
            }
        }

        private Dictionary<int, T> _dInventory = new Dictionary<int, T>();

        public T Get(int id)
        {
            lock (_dInventory)
            {
                try { return _dInventory[id].Clone() as T; } catch (KeyNotFoundException) { return null; }
            }
        }

        public bool Delete(int id)
        {
            lock (_dInventory)
            {
                return _dInventory.Remove(id);
            }
        }

        public bool Update(int id, T obj)
        {
            try
            {
                lock (_dInventory)
                {
                    _dInventory[id] = obj;
                }
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

            lock (_dInventory)
            {
                _dInventory.Add(id, obj);
            }

            return id;
        }

        public int[] ListIds()
        {
            int i = 0;

            lock (_dInventory)
            {
                var ids = new int[_dInventory.Count];

                foreach (var key in _dInventory.Keys) { ids[i++] = key; }

                return ids;
            }
        }

        public KeyValuePair<int, T>[] List()
        {
            int i = 0;

            lock (_dInventory)
            {
                var ivs = new KeyValuePair<int, T>[_dInventory.Count];

                foreach (var id in ListIds()) { ivs[i++] = new KeyValuePair<int, T>(id, _dInventory[id]); }

                return ivs;
            }
        }
    }
}
