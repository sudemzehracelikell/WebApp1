using System.ComponentModel.DataAnnotations;

namespace WebApp1.models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = "system";
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}