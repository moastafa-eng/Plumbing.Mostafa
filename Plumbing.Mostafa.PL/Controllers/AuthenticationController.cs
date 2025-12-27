// Ignore Spelling: Mostafa

using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.VewModels;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers.Identity.ModelStateHelper;
using ServiceLayer.Services.Identity.Abstract;

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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _iMapper;
        private readonly IAuthenticationCustomService _authenticationCustomService;
        private readonly IValidator<SignUpVM> _signUpValidator;
        private readonly IValidator<SignInVM> _signInValidator;
        private readonly IValidator<ForgotPasswordVM> _forgotPasswordValidator;
        private readonly IValidator<ResetPasswordVM> _resetPasswordValidator;

        public AuthenticationController(UserManager<AppUser> userManager, IValidator<SignUpVM> signUpValidator, 
            IMapper iMapper, IValidator<SignInVM> signInValidator, SignInManager<AppUser> signInManager, 
            IValidator<ForgotPasswordVM> forgotPasswordValidator, IValidator<ResetPasswordVM> resetPasswordValidator, 
            IAuthenticationCustomService authenticationCustomService)
        {
            _userManager = userManager;
            _signUpValidator = signUpValidator;
            _iMapper = iMapper;
            _signInValidator = signInValidator;
            _signInManager = signInManager;
            _forgotPasswordValidator = forgotPasswordValidator;
            _resetPasswordValidator = resetPasswordValidator;
            _authenticationCustomService = authenticationCustomService;
        }




        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM request)
        {
            var validation = await _signUpValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return View();
            }

            var user = _iMapper.Map<AppUser>(request);

            // UserManager handles password validation and hashing before saving.
            // AppUser doesn't store plain passwords.

            // CreateAcync => UserValidator - Password Validator
            var userCreateResult = await _userManager.CreateAsync(user, request.Password);

            if (!userCreateResult.Succeeded)
            {
                ViewBag.Result = "NotSucceed"; // Notify the view that the user creation process failed
                ModelState.AddModelErrorList(userCreateResult.Errors);
                return View();
            }


            return RedirectToAction("SignIn", "Authentication");
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM request, string? returnUrl = null)
        {
            // The user access the page that he wants to Access it before log in
            returnUrl = returnUrl ?? Url.Action("Index", "Dashboard", new { area = "Admin" });

            var Validation = await _signInValidator.ValidateAsync(request);

            if(!Validation.IsValid)
            {
                Validation.AddToModelState(this.ModelState);
                return View();
            }

            // => Fine user by email
            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            // Check if user exist
            if(hasUser == null)
            {
                ViewBag.Result = "Failed";
                ModelState.AddModelErrorList(new List<string> {"Email or Password is wrong"}); // "Or Password" for security reasons.
                return View();
            }

            // SignIn
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true); // true in last : Lock out in failure

            // => if SignIn Succeeded : 
            if(signInResult.Succeeded)
            {
                // ! it can not be null
                return Redirect(returnUrl!);
            }

            // => Lockout
            if(signInResult.IsLockedOut)
            {
                ViewBag.Result = "Lockout";
                ModelState.AddModelErrorList(new List<string> { "Your account has been locked out for 60 Second" });
                return View();
            }

            // => If Sign In Not Succeeded : 
            ViewBag.Result = "FailedSignIn";
            ModelState.AddModelErrorList(new List<string> {"Email or Password is wrong", $"Failed attempts{
                await _userManager.GetAccessFailedCountAsync(hasUser)}" }); // Number of attempts remaining.

            return View();
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM request)
        {
            // validation : contains any error if exist.
            var validation = await _forgotPasswordValidator.ValidateAsync(request);

            if(!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return View();
            }

            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if(hasUser == null)
            {
                ViewBag.Result = "UserDoen'tExist";
                ModelState.AddModelErrorList(new List<string> { "User does not exist!" });
                return View();
            }

            await _authenticationCustomService.CreateResetCredentialAndSend(hasUser, HttpContext, Url, request);

            return RedirectToAction("SignIn", "Authentication");
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token, List<string> errors)
        {
            TempData["UserId"] = userId;
            TempData["Token"] = token;

            if(errors.Any())
            {
                ViewBag.Result = "Errors";
                ModelState.AddModelErrorList(errors);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM request)
        {
            var userId = TempData["UserId"];
            var token = TempData["Token"];

            if(userId == null || token == null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }

            var validation = _resetPasswordValidator.Validate(request);

            if(!validation.IsValid)
            {
                List<string> errors = validation.Errors.Select(x => x.ErrorMessage).ToList();

                return RedirectToAction("ResetPassword", "Authentication", new {userId, token, errors});
            }

            var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);

            if(hasUser is null)
            {
                return RedirectToAction("SignIn", "Authentication");
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(hasUser!, token.ToString()!, request.Password);

            if(resetPasswordResult.Succeeded)
            {
                return RedirectToAction("SignIn", "Authentication");
            }
            else
            {
                List<string> errors = resetPasswordResult.Errors.Select(x => x.Description).ToList();

                return RedirectToAction("ResetPassword", "Authentication", new { userId, token, errors });
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
