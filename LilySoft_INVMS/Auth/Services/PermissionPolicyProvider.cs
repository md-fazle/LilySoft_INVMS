using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace LilySoft_INVMS.Auth.Services
{
    public class PermissionPolicyProvider: IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _backupPolicyProvider;

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _backupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy?> GetDefaultPolicyAsync()
            => _backupPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
            => _backupPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            // Build dynamic policy using the permission string
            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }
    }
}
