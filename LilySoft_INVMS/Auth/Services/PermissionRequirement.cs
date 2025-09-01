using Microsoft.AspNetCore.Authorization;

namespace LilySoft_INVMS.Auth.Services
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }
        public PermissionRequirement(string permission) => Permission = permission;


    }
}
