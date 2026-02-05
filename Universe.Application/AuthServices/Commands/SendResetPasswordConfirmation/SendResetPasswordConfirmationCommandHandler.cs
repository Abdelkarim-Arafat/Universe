
namespace Universe.Application.AuthServices.Commands.SendResetPasswordCodeAsync;

public class SendResetPasswordConfirmationCommandHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<SendResetPasswordConfirmationCommandHandler> logger,
    IEmailSender emailSender
    ) : IRequestHandler<SendResetPasswordConfirmationCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<SendResetPasswordConfirmationCommandHandler> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<Result> Handle(SendResetPasswordConfirmationCommand request, CancellationToken cancellationToken)
    {
        if(await _userManager.FindByNameAsync(request.UserName) is not { } user)
            return Result.Success();

        if (!user.EmailConfirmed)
            return Result.Failure(AuthErrors.EmailNotConfirmed);

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);

        _logger.LogInformation("Reset code: {code}", code);

        await _emailSender.SendResetPasswordEmail(user, code);

        return Result.Success();
    }
}
