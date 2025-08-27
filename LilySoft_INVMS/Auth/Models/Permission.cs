using System.ComponentModel.DataAnnotations;

namespace LilySoft_INVMS.Auth.Models
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Required, StringLength(150)]
        public string? PermissionName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        // Navigation
        public virtual ICollection<RolePermission>? RolePermissions { get; set; }

    }
}
