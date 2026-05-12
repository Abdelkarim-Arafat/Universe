
using Microsoft.EntityFrameworkCore;

namespace Universe.Application.AuthServices.Commands.ResetPassword;

internal class ResetPasswordCommandHandler(
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(x => x.passwordResetOtps)
            .SingleOrDefaultAsync(u => u.Email == request.Email);

        if (user is null || !user.EmailConfirmed)
            return Result.Failure(StudentErrors.NotFound);

        var otp = user.passwordResetOtps.OrderByDescending(x => x.CreatedAt).FirstOrDefault();

        if(otp is null || otp.IsExpired || !otp.IsVerified)
            return Result.Failure(AuthErrors.InvalidOrExpiredCode);
        
        otp.isUsed = true;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (!resetResult.Succeeded)
            return Result.Failure(AuthErrors.FailedChangedPassword);

        await _userManager.UpdateSecurityStampAsync(user);

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }
}
