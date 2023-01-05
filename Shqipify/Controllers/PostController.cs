using Microsoft.AspNetCore.Mvc;
using Shqipify.DAL;
using Shqipify.Models;
using Shqipify.Models.DBEnteties;

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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PostViewModel postData)
        {
            if(ModelState.IsValid)
            {
                var post = new Post()
                {
                    Title = postData.Title,
                    Description = postData.Description,
                    Image = postData.Image,
                    Author="Gazmir Feimi",
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
