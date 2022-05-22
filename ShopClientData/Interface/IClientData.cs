using System;
using System.Collections.Generic;
using System.Text;

namespace ShopClientData.Interface
{
    public interface IClientData
    {
        public delegate void OnObserved(string s);

        void Disconnect();
        string Interact(string req);
        public void RegisterObservedMessageCallback(OnObserved callback);
        public void RegisterObservedType(int id);
    }
}
