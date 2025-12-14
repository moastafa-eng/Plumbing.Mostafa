// Ignore Spelling: Mostafa

using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Plumbing.Mostafa.PL.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class UserAuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly IMapper _mapper;

        public UserAuthenticationController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _usermanager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> UserEdit()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity!.Name!);

            var userEditeVM = _mapper.Map<UserEditVM>(user);

            return View(userEditeVM);
        }
    }
}
