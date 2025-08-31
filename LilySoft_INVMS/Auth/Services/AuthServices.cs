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

        // Optional: Fetch all users
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        // NEW: Get user by email and password
        public async Task<Users?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            // Find user by email and active status
            var user = await _context.Users
                                     .Include(u => u.Role)
                                     .FirstOrDefaultAsync(u => u.email == email && u.isActive);

            if (user == null)
                return null;

            // Verify password using the same PasswordHasher
            // NEW: Add null check for user.password before calling VerifyHashedPassword
            var passwordHasher = new PasswordHasher<Users>();
            if (string.IsNullOrEmpty(user.password))
                return null;

            var result = passwordHasher.VerifyHashedPassword(user, user.password, password);

            if (result == PasswordVerificationResult.Success)
                return user;

            return null;
        }
    }
}
