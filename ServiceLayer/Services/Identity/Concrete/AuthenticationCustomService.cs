using Azure.Core;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Helpers.Identity.EmailHelper;
using ServiceLayer.Services.Identity.Abstract;
using System;
using System.Collections.Generic;

namespace ServiceLayer.Services.Identity.Concrete
{
    public class AuthenticationCustomService : IAuthenticationCustomService
    {
        private readonly IEmailSendMethod _emailSendMethod;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationCustomService(IEmailSendMethod emailSendMethod, UserManager<AppUser> userManager)
        {
            _emailSendMethod = emailSendMethod;
            _userManager = userManager;
        }

        public async Task CreateResetCredentialAndSend(AppUser user, HttpContext context, IUrlHelper url, ForgotPasswordVM request)
        {
            // => Create -> token <- to reset password
            // GeneratePasswordResetTokenAsync : return a unique token to specific user

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // => Url : Send this Url to -user email- to reset his password
            // and this is the Segments of Url Rout to ResentPassword page and it's contains :
            var passwordResetLink = url.Action("ResetPassword", "Authentication", new
            {
                userId = user.Id, // UserId : To know which user that he want's to reset his password
                token = passwordResetToken,
            }, context.Request.Scheme); // Protocol : Http/Https

            await _emailSendMethod.SendPasswordResetLinkWithToken(passwordResetLink!, request.Email);
        }
    }
}
