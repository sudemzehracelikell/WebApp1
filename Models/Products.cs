namespace WebApp1.models {
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public float KdvOrani { get; set; }
        public bool Status { get; set; }
        public float Price { get; set; }
        public int? Stock { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}