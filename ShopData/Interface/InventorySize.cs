using System;
using System.Collections.Generic;
using System.Text;
using ShopData.Interface;

namespace ShopData.Interface
{
    public struct InventorySize
    {
        public int width, height, depth;

        public InventorySize(int w, int h, int d) { width = w; height = h; depth = d; }
    }
}
