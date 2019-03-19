using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Services
{
    public class MyPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private DefaultAuthorizationPolicyProvider _parent;

        public MyPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            options.Value.AddPolicy(
                MyIdentityData.Policy_Add,
                p => p.RequireRole(MyIdentityData.SiteAdminRoleName, MyIdentityData.TopicAdminRoleName, MyIdentityData.ContributorRoleName));

            options.Value.AddPolicy(
                MyIdentityData.Policy_Delete,
                p => p.RequireRole(MyIdentityData.SiteAdminRoleName, MyIdentityData.TopicAdminRoleName));

            options.Value.AddPolicy(MyIdentityData.Policy_Admin,
                p => p.RequireRole(MyIdentityData.SiteAdminRoleName));
            

            _parent = new DefaultAuthorizationPolicyProvider(options);
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(MyIdentityData.Policy_NotBanned))
            {
                var topic = policyName.Substring(MyIdentityData.Policy_NotBanned.Length);
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new NotBannedRequirement(topic));

                return Task.FromResult(policy.Build());
            } else
            {
                return _parent.GetPolicyAsync(policyName);

            }
        }
    }
}
