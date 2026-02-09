
using static System.Net.WebRequestMethods;

namespace Universe.Application.AuthServices.Commands.SendResetPasswordCodeAsync;

public class VerificationRsetPasswordCodeCommandHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<VerificationRsetPasswordCodeCommandHandler> logger,
    IEmailSender emailSender
    ) : IRequestHandler<SendResetPasswordConfirmationCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<VerificationRsetPasswordCodeCommandHandler> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<Result> Handle(SendResetPasswordConfirmationCommand request, CancellationToken cancellationToken)
    {
        if(await _userManager.FindByNameAsync(request.UserName) is not { } user)
            return Result.Success();

        if(user.Email != request.Email)
            return Result.Success();

        if (!user.EmailConfirmed)
            return Result.Failure(AuthErrors.EmailNotConfirmed);

        var otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(otp)));

        user.passwordResetOtps.Add(new PasswordResetOtp
        {
            CodeHash = hash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
        });

        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Reset code: {code}", otp);

        await _emailSender.SendResetPasswordEmail(user , otp);

        return Result.Success();
    }
}
