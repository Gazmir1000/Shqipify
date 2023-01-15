using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shqipify.Areas.Identity.Data;
using Shqipify.DAL;
using Shqipify.Models.DBEnteties;
using Shqipify.Models;

namespace Shqipify.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
          private readonly PostDBContext _context;
        public CommentController(PostDBContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(int id)
        { 
            ViewData["postId"] = id;
            return View();
        }

        [Authorize]
        [HttpGet]
      
        public async Task<IActionResult> CreateAsync(CommentViewModel commentData)
        {
            var loggedUser = await _userManager.GetUserAsync(User);


            if (ModelState.IsValid && loggedUser != null)
            {
                var comment = new Comments()
                {
                    PostId = commentData.PostId,
                    User = loggedUser,
                    Text = commentData.Text


                };
                _context.Comments.Add(comment);
                _context.SaveChanges();
                TempData["successMessage"] = "Comment added successfully";
                return RedirectToAction("Post");

            }
            else
            {
                TempData["errorMessage"] = "Model data nnot valid!";
                return View("Post");

            }


        }
    }
}
