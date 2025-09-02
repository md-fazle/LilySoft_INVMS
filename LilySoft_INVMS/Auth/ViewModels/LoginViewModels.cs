using System.ComponentModel.DataAnnotations;

namespace LilySoft_INVMS.Auth.ViewModels
{
    public class LoginViewModels
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? password { get; set; }
        [Display(Name = "Remember me ?")]
        public bool rememberMe { get; set; }
    }
}
