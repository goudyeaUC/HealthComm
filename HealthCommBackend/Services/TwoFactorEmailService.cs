using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using HealthCommBackend.Models;
using MailKit;

namespace HealthCommBackend.Services
{
    public class TwoFactorEmailService : IEmailSender<ApplicationUser>
    {
        public async Task SendConfirmationLinkAsync(
            ApplicationUser user, string email, string confirmationLink)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HealthComm", "noreply@HealthComm.com"));
            message.To.Add(new MailboxAddress(user.UserName, email));
            message.Subject = "Confirm your email";
            message.Body = new TextPart("html") { Text = $"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>" };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("localhost", 1025, SecureSocketOptions.None);
                await client.SendAsync(message);

            }
        }        

    public Task SendPasswordResetLinkAsync(
        ApplicationUser user, string email, string resetLink)
    {
        // Send the password-reset link.
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(
        ApplicationUser user, string email, string resetCode)
    {
        // Send the password-reset code.
        throw new NotImplementedException();
    }
}
    }

