using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using HealthCommBackend.Models;

namespace HealthCommBackend.Services
{
    public class TwoFactorEmailService : IEmailSender<ApplicationUser>
    {
    public Task SendConfirmationLinkAsync(
        ApplicationUser user, string email, string confirmationLink)
    {

       var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HealthComm", "noreply@HealthComm.com"));
            message.To.Add(new MailboxAddress(user.UserName, email));
            message.Subject = "Confirm your email";
            message.Body = new TextPart("html") { Text = $"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>" };

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
}
