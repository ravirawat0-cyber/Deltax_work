using System;
using System.Collections.Generic;

namespace E_commerce.Models
{
    public class OrderRequest
    {
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ItemList> ItemLists { get; set; }
    }
}

