using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Services.Identity.Abstract
{
    public interface IAuthenticationCustomService
    {
        Task CreateResetCredentialAndSend(AppUser user, HttpContext context, IUrlHelper url, ForgotPasswordVM reques);
    }
}
