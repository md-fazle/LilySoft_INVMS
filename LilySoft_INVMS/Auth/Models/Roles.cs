using System.ComponentModel.DataAnnotations;

namespace LilySoft_INVMS.Auth.Models
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }

        [Required, StringLength(100)]
        public string? RoleName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        // Navigation
        public virtual ICollection<Users>? Users { get; set; }
        public virtual ICollection<RolePermission>? RolePermissions { get; set; } 
    }
}
