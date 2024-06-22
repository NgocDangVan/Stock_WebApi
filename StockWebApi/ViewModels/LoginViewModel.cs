using System.ComponentModel.DataAnnotations;

namespace StockWebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = "example@gmail.com";
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
    }
}
