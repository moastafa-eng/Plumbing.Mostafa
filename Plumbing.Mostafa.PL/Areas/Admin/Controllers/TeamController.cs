// Ignore Spelling: Mostafa

using EntityLayer.WebApplication.ViewModels.TeamViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }



        public async Task<IActionResult> GetAllTeamList()
        {
            var teamList = await _teamService.GetAllTeamListAsync();

            return View(teamList);
        }

        [HttpGet]
        public IActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(TeamAddVM request)
        {
            await _teamService.AddTeamAsync(request);

            return RedirectToAction("GetAllTeamList", "Team", new { area = ("Admin") });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTeam(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);

            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeam(TeamUpdateVM request)
        {
            await _teamService.UpdateTeamAsync(request);

            return RedirectToAction("GetAllTeamList", "Team", new { area = ("Admin") });
        }

        [HttpGet]
        public IActionResult DeleteTeam(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeamConfirmed(int id)
        {
            await _teamService.DeleteTeamAsync(id);

            return RedirectToAction("GetAllTeamList", "Team", new { area = ("Admin") });
        }
    }
}
