using System;

namespace E_commerce.Models
{
    public class Orders
    {
       public int CustomerID { get; set; }
       public DateTime OrderDate { get; set; }
       public Decimal TotalAmount { get; set; }
    }
}
