using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Interface
{
    public interface IRepo<T> where T : class
    {
        T Get(int id);
        bool Delete(int id);
        bool Update(int id, T obj);
        int Create(T obj);
        int[] ListIds();
        KeyValuePair<int, T>[] List();
    }
}
