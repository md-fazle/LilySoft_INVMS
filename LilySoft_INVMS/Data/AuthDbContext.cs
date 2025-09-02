using LilySoft_INVMS.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Data
{
    public class AuthDbContext : IdentityDbContext<Users>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
    }
}
