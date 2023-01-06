using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shqipify.Areas.Identity.Data;
using Shqipify.DAL;
using Shqipify.Models;
using Shqipify.Models.DBEnteties;
using System.Security.Claims;

namespace Shqipify.Controllers
{
    public class PostController : Controller
    {


        private readonly PostDBContext _context;
   

        public PostController(PostDBContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            if (posts != null)
            {
                List<PostViewModel> postList = new List<PostViewModel>();
                foreach (var post in posts)
                {
                    var temoPost = new PostViewModel()
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Description = post.Description,
                        Author = post.Author,
                        Image = post.Image,
                        CreatedTime = post.CreatedTime
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
         [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
      
        [Authorize]
        [HttpPost]


        public async Task<IActionResult> CreateAsync(PostViewModel postData)
        {

           

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //ClaimsPrincipal currentUser = this.User;
                //  var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                // AppUser user = await _userManager.FindByIdAsync(currentUserName);
                var post = new Post()
                {
                    Title = postData.Title,
                    Description = postData.Description,
                    Image = postData.Image,
                    Author= "Gazi",
                    UserId= userId,
                    CreatedTime=DateTime.Now,
                    

                };
                _context.Posts.Add(post);
                _context.SaveChanges();
                TempData["successMessage"] = "Post added successfully";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["errorMessage"] = "Model data nnot valid!";
                return View();

            }
         
        }


    }
}
