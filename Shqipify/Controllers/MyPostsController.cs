using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shqipify.Areas.Identity.Data;
using Shqipify.DAL;
using Shqipify.Models.DBEnteties;
using Shqipify.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shqipify.Controllers
{
    public class MyPostsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly PostDBContext _context;



        public MyPostsController(PostDBContext context, UserManager<AppUser> userManager)
        {
            this._context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public  IActionResult Index()
        {
            var loggedUser =  _userManager.GetUserId(User);
            var posts = _context.Posts.Where(p=>p.UserId==loggedUser).ToList();
            if (posts != null)
            {
                List<PostViewModel> postList = new List<PostViewModel>();

                foreach (var post in posts)
                {
                    List<Comments> comments = _context.Comments.Where(c => c.PostId == post.Id).ToList();
                    var temoPost = new PostViewModel()
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Description = post.Description,
                        Author = post.Author,
                        Image = post.Image,
                        CreatedTime = post.CreatedTime,
                        Comments = comments,
                        SelectedUni=post.university
                    };
                    postList.Add(temoPost);
                }
                return View(postList);
            }
            else
            {
                return View();
            }


        }

        [HttpGet]

        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
               
                var post = await _context.Posts.FirstOrDefaultAsync(u => u.Id == id);
                var model = new PostViewModel()
                {
                    Id = post.Id,
                    Title=post.Title,
                    Description=post.Description,
                    Author=post.Author,
                    SelectedUni=post.university,
                    CreatedTime=post.CreatedTime,
                    Image=post.Image,
                    UserId=post.UserId,

                };

                
                return View(model);
            }
            else
            {
                return View("Index");
            }

        }


        [HttpPost]


        public async Task<IActionResult> UpdateAsync(PostViewModel postData)
        {

            if (ModelState.IsValid)
            {
                var post = await _context.Posts.FindAsync(postData.Id);
                if (post != null)
                {
                    post.Id = postData.Id;
                    post.Image = postData.Image;
                    post.Author = postData.Author;
                    post.UserId = postData.UserId;
                    post.university = postData.SelectedUni;
                    post.Title = postData.Title;
                    post.Description = postData.Description;
                    post.CreatedTime = postData.CreatedTime;


                }
               await _context.SaveChangesAsync();
                TempData["successMessage"] = "Post edited successfully";
                return View("Index");

            }
            else
            {
                TempData["errorMessage"] = "Model data not valid!";
                return View();

            }

        }
        [HttpPost, ActionName("DeletePost")]


        public async Task<IActionResult> DeleteAsync(int id)
        {
            var uni = await _context.Posts.FindAsync(id);
            if (uni != null)
            {
                _context.Posts.Remove(uni);

            }
            await _context.SaveChangesAsync();
            TempData["successMessage"] = "Uni deleted successfully";
            return RedirectToAction(nameof(Index));

        }


    }
}
