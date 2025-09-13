using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.DBContext;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Auth.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly AuthDbContext _context;

        public UserPermissionService(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<UserPermissionsViewModel> GetUserPermissionsAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.userId == userId && u.isActive);

            if (user == null || user.Role == null)
            {
                throw new Exception("User or role not found");
            }

            var permissions = user.Role.RolePermissions?
                .Select(rp => new PermissionInfo
                {
                    PermissionId = rp.Permission.PermissionId,
                    PermissionName = rp.Permission.PermissionName,
                    Description = rp.Permission.Description
                })
                .ToList() ?? new List<PermissionInfo>();

            return new UserPermissionsViewModel
            {
                UserId = user.userId,
                UserName = user.userName,
                Email = user.email,
                RoleName = user.Role.RoleName,
                Permissions = permissions
            };
        }

        public async Task<List<PermissionInfo>> GetUserPermissionsListAsync(int userId)
        {
            var permissions = await _context.Users
                .Where(u => u.userId == userId && u.isActive)
                .SelectMany(u => u.Role.RolePermissions)
                .Select(rp => new PermissionInfo
                {
                    PermissionId = rp.Permission.PermissionId,
                    PermissionName = rp.Permission.PermissionName,
                    Description = rp.Permission.Description
                })
                .ToListAsync();

            return permissions;
        }

        public async Task<bool> HasPermissionAsync(int userId, string permissionName)
        {
            var hasPermission = await _context.Users
                .Where(u => u.userId == userId && u.isActive)
                .SelectMany(u => u.Role.RolePermissions)
                .AnyAsync(rp => rp.Permission.PermissionName == permissionName);

            return hasPermission;
        }


    }
}
