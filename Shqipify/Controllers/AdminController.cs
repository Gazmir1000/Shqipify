using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shqipify.Constants;
using Shqipify.DAL;
using Shqipify.Models;
using Shqipify.Models.DBEnteties;
using System.Security.Claims;

namespace Shqipify.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller

    {

        private readonly PostDBContext _context;


        public AdminController(PostDBContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var universities = _context.Universities.ToList();
                if (universities != null)
            {
                List<UniversityViewModel> universityList = new List<UniversityViewModel>();
                foreach (var uni in universities)
                {
                    var tempData = new UniversityViewModel()
                    {
                        Id= uni.Id,
                        Name= uni.Name,
                    };
                    universityList.Add(tempData);
                }
                return View(universityList);
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


        public async Task<IActionResult> CreateAsync(UniversityViewModel uniData)
        {



            if (ModelState.IsValid)
            {
               
                var uni = new University()
                {
                   Id=uniData.Id,
                   Name=uniData.Name


                };
                _context.Universities.Add(uni);
                _context.SaveChanges();
                TempData["successMessage"] = "Uni added successfully";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["errorMessage"] = "Model data not valid!";
                return View();

            }

        }


    }
}
