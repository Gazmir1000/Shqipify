using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Shqipify.Areas.Identity.Data;
using Shqipify.DAL;
using Shqipify.Models;
using Shqipify.Models.DBEnteties;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shqipify.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
       
        private readonly PostDBContext _context;



        public PostController(PostDBContext context, UserManager<AppUser> userManager)
        {
            this._context = context;
            _userManager = userManager;
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
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var universities = _context.Universities.ToList();
            var model = new PostViewModel();
            model.University = new List<SelectListItem>();
            foreach(var u in universities)
            {
                model.University.Add(new SelectListItem { Text = u.Name, Value = u.Name });
            }
            return View(model);
        }
      
        [Authorize]
        [HttpPost]


        public async Task<IActionResult> CreateAsync(PostViewModel postData)
        {
            var loggedUser = await _userManager.GetUserAsync(User);


            if (ModelState.IsValid && loggedUser!=null)
            {
                var post = new Post()
                {
                    Title = postData.Title,
                    Description = postData.Description,
                    Image = postData.Image,
                    Author= loggedUser.Firstname +" "+ loggedUser.Lastname,
                    UserId= loggedUser.Id,
                    CreatedTime=DateTime.Now,
                    university=postData.SelectedUni
                    

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


        public async Task<IActionResult> CommentAsync(CommentViewModel commentData)
        {
            var loggedUser = await _userManager.GetUserAsync(User);


            if (ModelState.IsValid && loggedUser != null)
            {
                var comment = new Comments()
                {
                    Id=commentData.Id,
                    PostId = commentData.PostId,
                    Author = loggedUser.Firstname + " "+ loggedUser.Lastname,
                    Text = commentData.Text

                };
                _context.Comments.Add(comment);
                _context.SaveChanges();
                TempData["successMessage"] = "Comment added successfully";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["errorMessage"] = "Model data nnot valid!";
                return View("Index");

            }

        }


    }
}
