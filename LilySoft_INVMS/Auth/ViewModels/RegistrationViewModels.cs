using System.ComponentModel.DataAnnotations;

namespace LilySoft_INVMS.Auth.ViewModels
{
    public class RegistrationViewModels
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(40,ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password and Confirm Password do not match.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
 
    }
}
