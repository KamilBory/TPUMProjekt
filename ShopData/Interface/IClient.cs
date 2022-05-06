using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Interface
{
    public interface IClient
    {
        string name { set; get; }
        string surname { set; get; }
        string password { set; get; }  
    }
}
