using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums
{
    public class MyIdentityData
    {
        public const string SiteAdminRoleName = "SiteAdmin";
        public const string TopicAdminRoleName = "TopicAdminof";
        public const string ContributorRoleName = "Contributor";

        public const string BlogPolicy_Add = "CanAddBlogPostsComments";
        public const string BlogPolicy_Delete = "CanDeleteBlogPosts";
        public const string BlogPolicy_Ban = "CanBanUsers";
        public const string BlogPolicy_Admin = "IsSiteAdmin";


        internal static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in new[] { SiteAdminRoleName, TopicAdminRoleName, ContributorRoleName })
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    roleManager.CreateAsync(role).GetAwaiter().GetResult();
                }
            }

            foreach (var userName in new[] { "alex@alex.com" })
            {
                var user = userManager.FindByNameAsync(userName).Result;
                if (user == null)
                {
                    user = new IdentityUser(userName);
                    user.Email = userName;
                    //user.SecurityStamp = Guid.NewGuid().ToString();
                    userManager.CreateAsync(user, "webdev").GetAwaiter().GetResult();
                }
                userManager.AddToRoleAsync(user, SiteAdminRoleName).GetAwaiter().GetResult();
                
            }
        }
    }
}
