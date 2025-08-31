using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.DBContext;
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
        // ✅ Get all users
        public async Task<List<Users>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // ✅ Check if email exists
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.email == email);
        }

        // ✅ Insert user
        public async Task InsertUserAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Get All Roles
        
        public async Task<List<Roles>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
