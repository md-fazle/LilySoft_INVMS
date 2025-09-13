using System.ComponentModel.DataAnnotations;

namespace LilySoft_INVMS.Auth.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string? email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        public bool RememberMe { get; set; }

        public DateTime loginTime { get; set; } = DateTime.Now;

    }
}
