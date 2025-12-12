using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ServiceLayer.Helpers.Identity.EmailHelper
{
    // I don't understand this topic.
    public interface IEmailSendMethod
    {
        Task SendPasswordResetLinkWithEmail(string passwordResetLink, string toEmail);
    }
    public class EmailSendMethod : IEmailSendMethod
    {
        private readonly GmailInformationVM _emailInfo;

        // IOption<GmailInformation> : Injecting email Information from AppSettings using IOptions
        public EmailSendMethod(IOptions<GmailInformationVM> emailInfo)
        {
            _emailInfo = emailInfo.Value; // .Value : Get instance from GmailInformationVM
        }

        public async Task SendPasswordResetLinkWithEmail(string passwordResetLink, string toEmail)
        {
            // smtpClient Configurations
            var smptClient = new SmtpClient();

            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.Host = _emailInfo.Host;
            smptClient.Port = 587;
            smptClient.UseDefaultCredentials = false;
            smptClient.Credentials = new NetworkCredential(_emailInfo.Email, _emailInfo.Password);
            smptClient.EnableSsl = true;

            // mailMessage Configurations
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailInfo.Email);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Local Host | Password Reset Link";
            mailMessage.Body = $@"<h4>Click the below link to reset your password</h4> 
                                <p><a href='{passwordResetLink}'>Reset Password</a></p>";
            mailMessage.IsBodyHtml = true;

            await smptClient.SendMailAsync(mailMessage);
        }
    }
}
