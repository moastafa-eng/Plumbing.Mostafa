using EntityLayer.Identity.ViewModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ServiceLayer.Helpers.Identity.EmailHelper
{
    // I don't understand this topic.
    public interface IEmailSendMethod
    {
        Task SendPasswordResetLinkWithToken(string passwordResetLink, string toEmail);
    }
    public class EmailSendMethod : IEmailSendMethod
    {
        private readonly GmailInformationVM _emailInfo;

        // IOption<GmailInformation> : Injecting email Information from AppSettings using IOptions
        public EmailSendMethod(IOptions<GmailInformationVM> emailInfo)
        {
            _emailInfo = emailInfo.Value; // .Value : Get instance from GmailInformationVM
        }

        public async Task SendPasswordResetLinkWithToken(string passwordResetLink, string toEmail)
        {
            #region Create an instance of the SmtpClient
            //Create an SmtpClient object: Initializes the client for sending emails via SMTP.

            //Set delivery method to network: Configures email to be sent over the network using the specified SMTP server.

            //Set the SMTP server host: Specifies the server address(e.g., smtp.gmail.com) for sending emails.

            //Set the SMTP port(587 for secure email submission): Uses port 587, which is standard for secure email communication.

            //Disable default credentials: Disables system default login credentials and allows custom credentials.

            //Set email account credentials(username and password): Defines the email account's username (email) and password for authentication.

            //Enable SSL for secure communication: Ensures encryption of the connection to the SMTP server for data security.
            #endregion

            var smptClient = new SmtpClient(); // SMTP (Simple Mail Transfer Protocol) 

            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.Host = _emailInfo.Host;
            smptClient.Port = 587;
            smptClient.UseDefaultCredentials = false;
            smptClient.Credentials = new NetworkCredential(_emailInfo.Email, _emailInfo.Password);
            smptClient.EnableSsl = true;

            // Create an instance of the mailMessage
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailInfo.Email);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Local Host | Password Reset Link";
            mailMessage.Body = $@"<h4>Click the below link to reset your password</h4> 
                                <p><a href='{passwordResetLink}'>Reset Password</a></p>";
            mailMessage.IsBodyHtml = true;

            await smptClient.SendMailAsync(mailMessage); // send mail with token
        }
    }
}
