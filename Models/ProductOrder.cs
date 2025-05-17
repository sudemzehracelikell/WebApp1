namespace WebApp1.models
{
    public class ProductOrder
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}