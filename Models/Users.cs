using System.ComponentModel.DataAnnotations;

namespace WebApp1.models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "İsim alanı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "İsim 2-100 karakter arasında olmalıdır")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public int? PhoneNumber { get; set; }

        public Boolean Status { get; set; } = true;

        [Required(ErrorMessage = "Kullanıcı tipi seçilmelidir")]
        public UserType UserType { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public enum UserType
    {
        Personal,
        Corporate,
        Distributor
    }
}