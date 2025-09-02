using System.ComponentModel.DataAnnotations;

namespace LilySoft_INVMS.Auth.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Current Password is required")]
        [StringLength(40, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Current Password and New Password cannot be the same.")]
        public string? password { get; set; }
        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string? newPassword { get; set; }
    }
}
