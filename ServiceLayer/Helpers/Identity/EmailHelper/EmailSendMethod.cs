using EntityLayer.Identity.Entities;

namespace ServiceLayer.Helpers.Identity.EmailHelper
{
    // I don't understand this topic.
    public interface IEmailSendMethod
    {
        Task SendPasswordResetLinkWithToken(string passwordResetLink, string token);
    }
    public class EmailSendMethod : IEmailSendMethod
    {
        public Task SendPasswordResetLinkWithToken(string passwordResetLink, string token)
        {
            throw new NotImplementedException();
        }
    }
}
