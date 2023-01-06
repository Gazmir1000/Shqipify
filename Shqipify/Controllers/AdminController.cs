using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shqipify.Constants;

namespace Shqipify.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
