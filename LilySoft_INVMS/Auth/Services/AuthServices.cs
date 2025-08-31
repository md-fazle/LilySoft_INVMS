using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Auth.Services
{
    public class AuthServices
    {
        private readonly AuthDbContext _context;

        public AuthServices(AuthDbContext context)
        {
            _context = context;
        }

        // Get all roles
        public async Task<List<Roles>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        // Check if email exists
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.email == email);
        }

        // Insert new user with isActive=true and hashed password
        public async Task InsertUserAsync(Users user)
        {
            if (string.IsNullOrWhiteSpace(user.password))
                throw new ArgumentException("Password cannot be null or empty.");

            user.isActive = true;

            var passwordHasher = new PasswordHasher<Users>();
            user.password = passwordHasher.HashPassword(user, user.password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Fetch all users including roles and permissions
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.Users
                                 .Include(u => u.Role)
                                 .ThenInclude(r => r.RolePermissions)
                                 .ThenInclude(rp => rp.Permission)
                                 .ToListAsync();
        }

        // Get user by email and password, including RolePermissions for claims
        public async Task<Users?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            // Include Role -> RolePermissions -> Permission
            var user = await _context.Users
                                     .Include(u => u.Role)
                                     .ThenInclude(r => r.RolePermissions!)
                                     .ThenInclude(rp => rp.Permission)
                                     .FirstOrDefaultAsync(u => u.email == email && u.isActive);

            if (user == null || string.IsNullOrEmpty(user.password))
                return null;

            // Verify hashed password
            var passwordHasher = new PasswordHasher<Users>();
            var result = passwordHasher.VerifyHashedPassword(user, user.password, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
