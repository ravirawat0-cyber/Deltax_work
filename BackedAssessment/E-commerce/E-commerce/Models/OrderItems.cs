namespace E_commerce.Models
{
    public class OrderItems
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
