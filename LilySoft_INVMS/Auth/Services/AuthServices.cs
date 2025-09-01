using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Auth.Services
{
    public class AuthServices
    {
        private readonly AuthDbContext _context;
        private readonly PasswordHasher<Users> _passwordHasher;

        public AuthServices(AuthDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Users>();
        }

        public async Task<List<Roles>> GetAllRolesAsync()
        {
            return await _context.Roles
                                 .Include(r => r.RolePermissions!)
                                 .ThenInclude(rp => rp.Permission)
                                 .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.email == email);
        }

        public async Task InsertUserAsync(Users user)
        {
            if (string.IsNullOrWhiteSpace(user.password))
                throw new ArgumentException("Password cannot be null or empty.");

            user.isActive = true;
            user.password = _passwordHasher.HashPassword(user, user.password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Users?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _context.Users
                                     .Include(u => u.Role)
                                     .ThenInclude(r => r.RolePermissions!)
                                     .ThenInclude(rp => rp.Permission)
                                     .FirstOrDefaultAsync(u => u.email == email && u.isActive);

            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.password, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<List<string>> GetUserPermissionsAsync(int userId)
        {
            var user = await _context.Users
                                     .Include(u => u.Role)
                                     .ThenInclude(r => r.RolePermissions!)
                                     .ThenInclude(rp => rp.Permission)
                                     .FirstOrDefaultAsync(u => u.userId == userId);

            if (user?.Role?.RolePermissions == null)
                return new List<string>();

            return user.Role.RolePermissions
                        .Where(rp => rp.Permission != null)
                        .Select(rp => rp.Permission!.PermissionName!)
                        .ToList();
        }
    }
}
