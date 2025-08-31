using LilySoft_INVMS.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.DBContext
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        // Define DbSets for your entities  
        public DbSet<Auth.Models.Roles> Roles { get; set; }
        public DbSet<Auth.Models.Permission> Permissions { get; set; }
        public DbSet<Auth.Models.RolePermission> RolePermissions { get; set; }

        public DbSet<Auth.Models.Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // user -> Role (one to many)  
            modelBuilder.Entity<Auth.Models.Users>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict); // Fix: Replace WillCascadeOnDelete(false) with OnDelete(DeleteBehavior.Restrict)  

            // Role -> RolePermission (one to many)  
            modelBuilder.Entity<Auth.Models.RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<Auth.Models.RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

        }
    }
}
