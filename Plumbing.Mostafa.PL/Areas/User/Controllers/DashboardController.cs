// Ignore Spelling: Mostafa

using Microsoft.AspNetCore.Mvc;

namespace Plumbing.Mostafa.PL.Areas.User.Controllers
{
    [Area("User")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
