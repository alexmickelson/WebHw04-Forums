using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebHw04_Forums.Data;
using WebHw04_Forums.Models;
using WebHw04_Forums.Services;

namespace WebHw04_Forums.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthorizationPolicyProvider _policyProvider;
        private readonly IAuthorizationService _authorization;

        public TopicsController(ApplicationDbContext context,
                                UserManager<IdentityUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                IAuthorizationPolicyProvider policyProvider,
                                IAuthorizationService authorization)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _policyProvider = policyProvider;
            _authorization = authorization;
        }


        public async Task<string> Sidebar()
        {
            var topics = await _context.Topics.ToListAsync();

            string html = "";

            foreach(var t in topics)
            {

                html = html + $"<a href='/topics/details?id={t.Name}' class='list-group-item list-group-item-action bg-light'>{t.Name}</a>";
            }

            return html;
        }


        // GET: Topics
        public async Task<IActionResult> Index()
        {

            return View(await _context.Topics.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(string id)
        {

            var p = await _policyProvider.GetPolicyAsync(MyIdentityData.Policy_NotBanned + "afds");
            var a = p.Requirements;
            
            
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Posts)
                .FirstOrDefaultAsync(m => m.Name == id);

            if (topic == null)
            {
                return NotFound();
            }

            if (topic.Posts == null)
            {
                topic.Posts = new List<Post>();
            }

            return View(topic);
        }

        // GET: Topics/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }


        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Topic topic)
        {
            if (ModelState.IsValid && _context.Topics.Find(topic.Name) == null )
            {
                
                _context.Add(topic);
                await _context.SaveChangesAsync();

                var roleName = MyIdentityData.TopicAdminRoleName + topic.Name;
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(role);
                    await _policyProvider.GetPolicyAsync(MyIdentityData.Policy_TopicAdmin+topic.Name);
                }
                

                roleName = MyIdentityData.BannedRoleName + topic.Name;
                role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(role);
                    await _policyProvider.GetPolicyAsync(MyIdentityData.Policy_NotBanned + topic.Name);
                }
                


                return RedirectToAction(nameof(Index));
            }


            return View(topic);
        }


        private bool TopicExists(string id)
        {
            return _context.Topics.Any(e => e.Name == id);
        }
    }
}
