using Universe.Core.Entities;

namespace Universe.Core.Interfaces;

public interface IEmailSender
{
    Task SendConfirmationEmail(ApplicationUser user, string code);
    Task SendResetPasswordEmail(ApplicationUser user, string code);
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
