using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Interfaces;

namespace Universe.Infrastructure.Messaging.Email;

public class EmailSender(
    IOptions<MailSettings> options,
    ILogger<EmailSender> logger,
    IHttpContextAccessor http) : IEmailSender
{
    private readonly MailSettings _mailSettings = options.Value;
    private readonly ILogger<EmailSender> _logger = logger;
    private readonly IHttpContextAccessor _httpContextAccessor = http;

    public async Task SendConfirmationEmail(ApplicationUser user, string code)
    {
        var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
            templateModel: new Dictionary<string, string>
            {
                { "{{name}}", user.Name },
                    { "{{action_url}}", $"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
            }
        );

        await SendEmailAsync(user.Email!, "✅ Survey Basket: Email Confirmation", emailBody);

        await Task.CompletedTask;
    }

    public async Task SendResetPasswordEmail(ApplicationUser user, string code)
    {
        var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
            templateModel: new Dictionary<string, string>
            {
                { "{{name}}", user.Name },
                { "{{action_url}}", $"{origin}/auth/forgetPassword?email={user.Email}&code={code}" }
            }
        );

        await SendEmailAsync(user.Email!, "✅ Universe: Change Password", emailBody);

        await Task.CompletedTask;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail),
            Subject = subject
        };

        message.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder { HtmlBody = htmlMessage };

        message.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();

        _logger.LogInformation("Sending email to {email}", email);

        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
    }
}
