using System.ComponentModel.DataAnnotations;

namespace LilySoft_INVMS.Auth.ViewModels
{
    public class VerifyEmailVewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        

    }
}
