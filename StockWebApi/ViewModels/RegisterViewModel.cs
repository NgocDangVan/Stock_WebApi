using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockWebApi.ViewModels
{
    public class RegisterViewModel
    {
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = "example@gmail.com";
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
        [Required(ErrorMessage = "Phone is required")]
        public string Phone = "0989675786";
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Country { get; set; }
    }
}
