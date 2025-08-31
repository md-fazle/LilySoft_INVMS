using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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


    }
}
