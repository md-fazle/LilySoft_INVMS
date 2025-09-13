namespace LilySoft_INVMS.Auth.Models
{
    public class UserPermissionsViewModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public List<PermissionInfo> Permissions { get; set; } = new List<PermissionInfo>();
    }
    public class PermissionInfo
    {
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public string? Description { get; set; }
    }
}
