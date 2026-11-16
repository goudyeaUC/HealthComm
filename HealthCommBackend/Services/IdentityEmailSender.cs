namespace HealthCommBackend.Services;
using Microsoft.AspNetCore.Identity;
using HealthCommBackend.Models;
using HealthCommBackend.Data;
using MimeKit;
using MailKit.Net.Smtp;

public class IdentityEmailSender : IEmailSender<ApplicationUser>
    {
        private static Task ConfigWarning() => throw new InvalidOperationException("WARNING: Email sending is not configured at this time.");
    public Task SendEmailAsync(ApplicationUser user, string subject, string message, string email) {
         return Task.CompletedTask; }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email,string resetLink)
    {
        return Task.CompletedTask;
    }

    public Task SendConfirmationLinkAsync(ApplicationUser user,string email, string confirmationLink)
    {
        return Task.CompletedTask;
    }
    
    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        return Task.CompletedTask;
    }

}
