using System;

namespace ShopData.Interface
{
    public interface IClient : ICloneable
    {
        string name { set; get; }
        string surname { set; get; }
        string password { set; get; }  
    }
}
