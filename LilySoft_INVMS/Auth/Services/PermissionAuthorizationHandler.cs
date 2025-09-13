using Microsoft.AspNetCore.Authorization;

namespace LilySoft_INVMS.Auth.Services
{
    public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            // Check if user has the permission claim
            if (context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
