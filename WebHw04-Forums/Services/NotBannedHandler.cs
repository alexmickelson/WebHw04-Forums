using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Services
{
    public class NotBannedHandler : AuthorizationHandler<NotBannedRequirement>
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;

        public NotBannedHandler(RoleManager<IdentityRole> roleManager,
                                UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                                                        NotBannedRequirement requirement)
        {
            var userTask = _userManager.GetUserAsync(context.User);
            userTask.Wait();
            var user = userTask.Result;
            var rolesTask = _userManager.IsInRoleAsync(user, MyIdentityData.getBannedRole(requirement.role));
            rolesTask.Wait();
            
           
            if ( !rolesTask.Result )
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            
            return Task.CompletedTask;
        }
    }
}
