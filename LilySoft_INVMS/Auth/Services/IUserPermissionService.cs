using LilySoft_INVMS.Auth.Models;

namespace LilySoft_INVMS.Auth.Services
{
    public class IUserPermissionService
    {
        Task<UserPermissionsViewModel> GetUserPermissionsAsync(int userId);
        Task<List<PermissionInfo>> GetUserPermissionsListAsync(int userId);
        Task<bool> HasPermissionAsync(int userId, string permissionName);
    }
}
