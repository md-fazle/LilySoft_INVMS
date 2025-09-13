using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LilySoft_INVMS.Auth.Models
{
    public class RolePermission
    {
        [Key]
        public int RolePermissionId { get; set; }

        // Foreign Keys
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles? Role { get; set; }

        [ForeignKey("PermissionId")]
        public virtual Permission? Permission { get; set; }
       

    }
}
