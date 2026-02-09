
using Microsoft.EntityFrameworkCore;
using Universe.Core.Errors;

namespace Universe.Application.AuthServices.Commands.VerificationResetPasswordCode;

public class VerificationRsetPasswordCodeCommandHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<VerificationRsetPasswordCodeCommandHandler> logger
    ) : IRequestHandler<VerificationResetPasswordCodeCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<VerificationRsetPasswordCodeCommandHandler> _logger = logger;

    public async Task<Result> Handle(VerificationResetPasswordCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(x => x.passwordResetOtps)
            .SingleOrDefaultAsync(x => x.Email == request.Email , cancellationToken);

        if (user is null || user.IsDeleted) 
            return Result.Failure(AuthErrors.UserNotFound);

        var otp = user.passwordResetOtps
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefault();

        if(otp is null || otp.IsExpired || otp.IsVerified)
            return Result.Failure(AuthErrors.InvalidOrExpiredCode);

        var inputHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(request.Code)));

        if(inputHash != otp.CodeHash)
        {
            otp.Attempts++;
            await _userManager.UpdateAsync(user);
            return Result.Failure(AuthErrors.InvalidOrExpiredCode);
        }
        otp.IsVerified = true;
        await _userManager.UpdateAsync(user);
        return Result.Success();
    }
}