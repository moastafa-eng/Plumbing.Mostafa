// Ignore Spelling: Mostafa

using AutoMapper;
using CoreLayer.Enumerators;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers.Generic.Image;
using ServiceLayer.Helpers.Identity.ModelStateHelper;

namespace Plumbing.Mostafa.PL.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class UserAuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly IMapper _mapper;
        private readonly IValidator<UserEditVM> _userEditValidator;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IImageHelper _imageHelper;

        public UserAuthenticationController(UserManager<AppUser> userManager, IMapper mapper,
            IValidator<UserEditVM> userEditValidator, SignInManager<AppUser> signInManager, IImageHelper imageHelper)
        {
            _usermanager = userManager;
            _mapper = mapper;
            _userEditValidator = userEditValidator;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity!.Name!);

            var userEditeVM = _mapper.Map<UserEditVM>(user);

            return View(userEditeVM);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditVM request)
        {
            var user = await _usermanager.FindByNameAsync(User.Identity!.Name!);

            var validation = _userEditValidator.Validate(request);

            if(!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return View(request);
            }

            var checkPassword = await _usermanager.CheckPasswordAsync(user!, request.Password);

            if(!checkPassword)
            {
                ViewBag.Result = "Failed";
                ModelState.AddModelErrorList(new List<string> { "Wrong Password!" });

                return View();
            }

            if(request.NewPassword is not null)
            {
                var changePassword = await _usermanager.ChangePasswordAsync(user!, request.Password, request.NewPassword);

                if(!changePassword.Succeeded)
                {
                    ViewBag.Result = "Failed";
                    ModelState.AddModelErrorList(changePassword.Errors);

                    return View();
                }
            }

            var oldFileName = user!.FileName;
            var oldFileType = user!.FileType;

            if(request.Photo is not null)
            {
                var image = await _imageHelper.ImageUpload(request.Photo, ImageType.identity, null);
                request.FileName = DateTime.Now.ToString();
                request.FileType = DateTime.Now.ToString();
            }

            else
            {
                request.FileName = oldFileName;
                request.FileType = oldFileType;
            }

            var updateUserResult = await _usermanager.UpdateAsync(_mapper.Map(request, user));
            
            if(!updateUserResult.Succeeded)
            {
                if(request.Photo is not null)
                {
                    if(oldFileName is not null)
                    {
                        // Delete Image.
                    }
                }

                await _usermanager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Dashboard", new { Area = "User" });
            }

            if(request.FileName is not null)
            {
                // Image delete
            }

            // this extra section must be above of update user section 
            if(request.NewPassword is not null)
            {
                await _usermanager.ChangePasswordAsync(user!, request.NewPassword, request.Password);
                await _usermanager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, false);
            }


            ViewBag.UserName = user.UserName;

            return View();
        }
    }
}
