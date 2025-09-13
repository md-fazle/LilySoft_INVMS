using Microsoft.AspNetCore.Identity;

namespace LilySoft_INVMS.Auth.Models
{
    public class Users:IdentityUser
    {

        public string? FullName { get; set; }

    }
}
