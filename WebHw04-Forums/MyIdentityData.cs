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
        public const string TopicAdminRoleName = "TopicAdmin";
        public const string ContributorRoleName = "Contributor";

        public const string BlogPolicy_Add = "CanAddBlogPostsComments";
        public const string BlogPolicy_Delete = "CanDeleteBlogPosts";
        public const string BlogPolicy_Ban = "CanBanUsers";


        //internal static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        //{
        //    foreach (var roleName in new[] { AdminRoleName, EditorRoleName, ContributorRoleName })
        //    {
        //        var role = roleManager.FindByNameAsync(roleName).Result;
        //        if (role == null)
        //        {
        //            role = new IdentityRole(roleName);
        //            roleManager.CreateAsync(role).GetAwaiter().GetResult();
        //        }
        //    }

        //    foreach (var userName in new[] { "admin@lsu.edu", "editor@lsu.edu", "contributor@lsu.edu" })
        //    {
        //        var user = userManager.FindByNameAsync(userName).Result;
        //        if (user == null)
        //        {
        //            user = new IdentityUser(userName);
        //            user.Email = userName;
        //            //user.SecurityStamp = Guid.NewGuid().ToString();
        //            userManager.CreateAsync(user, "P@ssword1").GetAwaiter().GetResult();
        //        }
        //        if (userName.StartsWith("admin"))
        //        {
        //            userManager.AddToRoleAsync(user, AdminRoleName).GetAwaiter().GetResult();
        //        }
        //        if (userName.StartsWith("editor"))
        //        {
        //            userManager.AddToRoleAsync(user, EditorRoleName).GetAwaiter().GetResult();
        //        }
        //        if (userName.StartsWith("contributor"))
        //        {
        //            userManager.AddToRoleAsync(user, ContributorRoleName).GetAwaiter().GetResult();
        //        }
        //    }
        //}
    }
}
