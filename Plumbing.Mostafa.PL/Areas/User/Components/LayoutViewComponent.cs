using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Plumbing.Mostafa.PL.Areas.User.Components
{
    [Authorize]
    [Area("User")]
    public class LayoutViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public LayoutViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // This method is automatically called when the Layout ViewComponent is invoked.
        // It prepares and sends the user's profile picture FileName to the View.
        //
        // By default, ASP.NET Core will look for the View at:
        // Views/Shared/Components/Layout/Default.cshtml
        //
        // The returned UserPictureVM will be used as the Model in that View.
        public async Task<IViewComponentResult> InvokeAsync(string username)
        {
            // If no username is provided, use the currently logged-in user's username
            if (username is null)
                username = User.Identity!.Name!;

            // Retrieve the user object from the database using UserManager
            var user = await _userManager.FindByNameAsync(username);

            // If the user does not have a profile picture,
            // send the default image name to the View
            if (user!.FileName is null)
            {
                return View(new UserPictureVM
                {
                    FileName = "Default"
                });
            }

            // If the user has a profile picture,
            // send the actual FileName to the View
            return View(new UserPictureVM
            {
                FileName = user.FileName
            });
        }
    }
}
