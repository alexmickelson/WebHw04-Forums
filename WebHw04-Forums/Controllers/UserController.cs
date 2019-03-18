using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHw04_Forums.Data;

namespace WebHw04_Forums.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context,
                                UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            ViewBag.TopicAmount = await _context.Topics.Where(t => t.Admins.Contains(user)).CountAsync();
            ViewBag.PostAmount = await _context.Post.Where(p=>p.UserId == user.Id).CountAsync();
            ViewBag.CommentAmount = await _context.Comment.Where(c => c.UserId == user.Id).CountAsync();
            

            return View(user);
        }
    }
}