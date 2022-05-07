using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace ShopPresentation.Model
{
    public class Order
    {
        public class Entry
        {
            public int choiceId { get; set; }
            public int offerId { get; set; }
            public string name { get; set; }
            public int count { get; set; }
            public int price { get; set; }
            public int sumPrice { get; set; }
        }

        public int id { get; set; }
        public ObservableCollection<Entry> entries { get; set; }
    }
}
