using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shqipify.Areas.Identity.Data;
using Shqipify.DAL;
using Shqipify.Models.DBEnteties;
using Shqipify.Models;

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
        
    }
}
