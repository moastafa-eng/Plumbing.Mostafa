// Ignore Spelling: Mostafa

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Plumbing.Mostafa.PL.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
