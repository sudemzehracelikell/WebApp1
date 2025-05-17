using System.ComponentModel.DataAnnotations;

namespace WebApp1.models
{
    public class Brand : BaseEntity
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Marka adı zorunludur")]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}