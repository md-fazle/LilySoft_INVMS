using Microsoft.AspNetCore.Authorization;

namespace LilySoft_INVMS.Auth.Services
{
    public class HasPermissionAttribute: AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
