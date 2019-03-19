using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHw04_Forums.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using WebHw04_Forums.Services;

namespace WebHw04_Forums
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddScoped<IServiceCollection, service>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(
                    o =>{
                        o.Password.RequireDigit = false;
                        o.Password.RequireLowercase = false;
                        o.Password.RequireNonAlphanumeric = false;
                        o.Password.RequireUppercase = false;
                        o.Password.RequiredUniqueChars = 0;
                        o.Password.RequiredLength = 0;
                    })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAuthorization(o =>
            //{
            //    o.AddPolicy(MyIdentityData.Policy_Add, p => p.RequireRole(MyIdentityData.SiteAdminRoleName, MyIdentityData.TopicAdminRoleName, MyIdentityData.ContributorRoleName));
            //    o.AddPolicy(MyIdentityData.Policy_Delete, p => p.RequireRole(MyIdentityData.SiteAdminRoleName, MyIdentityData.TopicAdminRoleName));
            //    o.AddPolicy(MyIdentityData.Policy_Admin, p => p.RequireRole(MyIdentityData.SiteAdminRoleName));
            //    o.AddPolicy(MyIdentityData.Policy_NotBanned, p => p.AddRequirements(new NotBannedRequirement()));
            //});

            services.AddTransient<IAuthorizationPolicyProvider, MyPolicyProvider>();
            services.AddTransient<IAuthorizationHandler, NotBannedHandler>();

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                            IHostingEnvironment env,
                            UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            MyIdentityData.SeedData(userManager, roleManager);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
