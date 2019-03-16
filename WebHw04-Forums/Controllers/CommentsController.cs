using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHw04_Forums.Data;
using WebHw04_Forums.Models;

namespace WebHw04_Forums.Controllers
{
    public class CommentsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> getComments()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<List<Comment>> getComments(int postId, int? parentComment)
        {
            return await _context.Comment.Where(c => c.ParentId == parentComment && c.PostId == postId).ToListAsync();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Time,ParentId,Content,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }
            return View(comment);
        }
    }
}
