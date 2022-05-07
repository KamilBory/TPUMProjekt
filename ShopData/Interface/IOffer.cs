using System;

namespace ShopData.Interface
{
    public interface IOffer : ICloneable
    {
        int sellPrice { get; set; }
        string name { get; set; }
        string description { get; set; }
        int count { get; set; }
    }
}
