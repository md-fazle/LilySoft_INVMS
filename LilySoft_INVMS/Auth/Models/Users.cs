using System.ComponentModel.DataAnnotations;

namespace LilySoft_INVMS.Auth.Models
{
    public class Users
    {
        [Key]
        public int userId { get; set; }

        public string? userName { get; set; } 

        public string? password { get; set; }

        [Required, StringLength(150)]
        public string? email { get; set; }

        [Required, StringLength(15)]
        public string? phoneNumber { get; set; }    
            
        public bool isActive { get; set; }  


        public int RoleId { get; set; }

        public Roles ? Role { get; set; } 
    }
}
