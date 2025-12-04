// Ignore Spelling: Mostafa

using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.VewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers.Identity;

namespace Plumbing.Mostafa.PL.Controllers
{
    public class AuthenticationController : Controller
    {
        #region UserManager = Service
        // UserManager<AppUser> is a built-in Identity service responsible for managing users.
        // It provides ready-made functions to:
        // - Create, update, and delete users
        // - Validate passwords and manage password hashing
        // - Handle email/phone confirmation tokens
        // - Manage user lockout, access failed count, and security features
        // - Work with roles and user claims
        // This service is automatically registered in the DI container via AddIdentity().
        #endregion
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<SignUpVM> _signUpValidator;
        private readonly IMapper _iMapper;

        public AuthenticationController(UserManager<AppUser> userManager, IValidator<SignUpVM> signUpValidator, 
            IMapper iMapper)
        {
            _userManager = userManager;
            _signUpValidator = signUpValidator;
            _iMapper = iMapper;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        public async Task<IActionResult> SignUp(SignUpVM request)
        {
            var validation = await _signUpValidator.ValidateAsync(request);

            if(!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return View();
            }

            var user = _iMapper.Map<AppUser>(request);

            // UserManager handles password validation and hashing before saving.
            // AppUser doesn't store plain passwords.

            // CreateAcync => UserValidator - Password Validator
            var userCreateResult = await _userManager.CreateAsync(user, request.Password);

            if(!userCreateResult.Succeeded)
            {
                ModelState.AddModelErrorList(userCreateResult.Errors);
                return View();
            }

            return RedirectToAction("LogIn", "Authentication");

        }
    }
}
