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
            var Logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                Logger.LogInformation("Starting database is createing...");
                await context.Database.EnsureCreatedAsync();


                // Seed Roles
                Logger.LogInformation("Seeding roles...");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "Managers");
                await AddRoleAsync(roleManager, "Staff");
                await AddRoleAsync(roleManager, "Viewer");

                Logger.LogInformation("Seeding admin Users...");

                //Add Admin User
                var adminEmail = "admin@lilysoft.com";
                if(await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new Users
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        FullName = "Md. Fazle Rabbi",
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while seeding the database.");
            }

        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
             if(await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
        }
    }
}
