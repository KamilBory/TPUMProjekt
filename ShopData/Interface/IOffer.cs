using System;
using System.Collections.Generic;
using System.Text;

namespace ShopData.Interface
{
    public interface IOffer
    {
        int sellPrice { get; set; }
        string name { get; set; }
        string description { get; set; }
        int count { get; set; }
    }
}
