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
        public const string TopicAdminRoleName = "TopicAdmin: ";
        public const string BannedRoleName = "BannedFrom: ";
        public const string ContributorRoleName = "Contributor";

        public const string Policy_Add = "CanAddBlogPostsComments";
        public const string Policy_Delete = "CanDeleteBlogPosts";
        public const string Policy_NotBanned = "NotBannedFrom: ";
        public const string Policy_TopicAdmin = "TopicAdminFor: ";
        public const string Policy_Admin = "IsSiteAdmin";

        public static string getBannedRole(string topic)
        {
            return BannedRoleName + topic;
        }
        public static string getAdminRole(string topic)
        {
            return TopicAdminRoleName + topic;
        }

        internal static void SeedData(UserManager<IdentityUser> userManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in new[] { SiteAdminRoleName, ContributorRoleName })
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
