using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.Data;
using Microsoft.AspNetCore.Identity;

namespace LilySoft_INVMS.Auth.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                logger.LogInformation("Starting database creation...");
                await context.Database.EnsureCreatedAsync();

                // Seed Roles
                logger.LogInformation("Seeding roles...");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "Managers");
                await AddRoleAsync(roleManager, "Staff");
                await AddRoleAsync(roleManager, "Viewer");

                logger.LogInformation("Seeding default users...");

                // Admin User
                await AddUserAsync(userManager, "admin@lilysoft.com", "Admin@123", "Md. Fazle Rabbi", "Admin");

                // Manager User
                await AddUserAsync(userManager, "manager@lilysoft.com", "Manager@123", "Aziza Taseen", "Managers");

                // Staff User
                await AddUserAsync(userManager, "staff@lilysoft.com", "Staff@123", "Staff User", "Staff");

                // Viewer User
                await AddUserAsync(userManager, "viewer@lilysoft.com", "Viewer@123", "Viewer User", "Viewer");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName)) // ✅ create only if not exists
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private static async Task AddUserAsync(UserManager<Users> userManager, string email, string password, string fullName, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new Users
                {
                    UserName = email, // ✅ use email as username for easy login
                    Email = email,
                    FullName = fullName,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed to create user '{email}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
