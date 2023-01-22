using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shqipify.Constants;
using Shqipify.DAL;
using Shqipify.Models;
using Shqipify.Models.DBEnteties;
using System.Security.Claims;

namespace Shqipify.Controllers
{
    [Authorize(Roles = "Admin")]
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
                        Id = uni.Id,
                        Name = uni.Name,
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
                    Id = uniData.Id,
                    Name = uniData.Name


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

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var uni = await _context.Universities.FirstOrDefaultAsync(u => u.Id == id);
                var model = new UniversityViewModel()
                {
                    Id = uni.Id,
                    Name = uni.Name
                };
                return View(model);
            }
            else
            {
                return View("Index");
            }
      
        }


        [HttpPost]


        public async Task<IActionResult> EditAsync(UniversityViewModel uniData)
        {

            if (ModelState.IsValid)
            {
                var uni = await _context.Universities.FindAsync(uniData.Id);
                if (uni != null)
                {
                    uni.Id = uniData.Id;
                    uni.Name = uniData.Name;
                }
                _context.SaveChangesAsync();
                TempData["successMessage"] = "Uni edited successfully";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["errorMessage"] = "Model data not valid!";
                return View();

            }

        }
        [HttpPost,ActionName("Delete")]


        public async Task<IActionResult> DeleteAsync(int id)
        {
            var uni = await _context.Universities.FindAsync(id);
            if (uni != null)
            {
                _context.Universities.Remove(uni);

            }
                await _context.SaveChangesAsync();
                TempData["successMessage"] = "Uni deleted successfully";
                return RedirectToAction(nameof(Index));

        }
    }
}
