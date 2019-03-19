using Microsoft.AspNetCore.Authorization;
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
        private readonly IAuthorizationService _authorization;

        public CommentsController(ApplicationDbContext context,
                                IAuthorizationService authorization)
        {
            _context = context;
            _authorization = authorization;
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
            var topicname = _context.Topics.Where(t => t.Posts.Where(p => p.Id == comment.PostId).Any()).First().Name;
            if ((await _authorization.AuthorizeAsync(User, MyIdentityData.Policy_NotBanned +
                        _context.Topics.Where(t => t.Posts.Where(p => p.Id == comment.PostId).Any()).First().Name)).Succeeded)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Posts", new { id = comment.PostId });
                }
                return View(comment);

            }
            else
            {
                return Redirect("/home/unauthorized");
            }
        }
    }
}
