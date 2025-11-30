// Ignore Spelling: Mostafa

using EntityLayer.WebApplication.ViewModels.TeamViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace Plumbing.Mostafa.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IValidator<TeamAddVM> _addValidation;
        private readonly IValidator<TeamUpdateVM> _updateValidation;

        public TeamController(ITeamService teamService, IValidator<TeamAddVM> addValidation, 
            IValidator<TeamUpdateVM> updateValidation)
        {
            _teamService = teamService;
            _addValidation = addValidation;
            _updateValidation = updateValidation;
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
            var validation = await _addValidation.ValidateAsync(request);

            if(validation.IsValid)
            {
                await _teamService.AddTeamAsync(request);

                return RedirectToAction("GetAllTeamList", "Team", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
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
            var validation = await _updateValidation.ValidateAsync(request);

            if (validation.IsValid)
            {
                await _teamService.UpdateTeamAsync(request);

                return RedirectToAction("GetAllTeamList", "Team", new { area = ("Admin") });
            }

            validation.AddToModelState(this.ModelState);

            return View();
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
