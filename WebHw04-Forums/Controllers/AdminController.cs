﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHw04_Forums.Data;
using WebHw04_Forums.Models;

namespace WebHw04_Forums.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context,
                               UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Policy = MyIdentityData.Policy_Admin)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewBag.TopicAmount = await _context.Topics.CountAsync();
            ViewBag.PostAmount = await _context.Post.CountAsync();
            ViewBag.CommentAmount = await _context.Comment.CountAsync();
            ViewBag.UserAmount = await _context.Users.CountAsync();



            return View();
        }

        [Authorize(Policy = MyIdentityData.Policy_Admin)]
        public async Task<IActionResult> Users()
        {
            List<UserViewModel> models = new List<UserViewModel>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                models.Add(new UserViewModel() {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = (List<string>)await _userManager.GetRolesAsync(user)
                });
            }

            var Roles = await _context.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.Roles = Roles;
            return View(models);
        }


        [HttpPost]
        [Authorize(Policy = MyIdentityData.Policy_Admin)]
        public async Task<IActionResult> Users(string id, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            var user = await _userManager.FindByIdAsync(id);
            //var newAdmin = _userManager.Users.Where(u => u.Id == id).First();

            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);

                return RedirectToAction(nameof(Users));
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}