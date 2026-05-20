using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.ResetUserPassword;

public class ResetUserPasswordCommandHandler(
    UserManager<ApplicationUser> userManager
) : IRequestHandler<ResetUserPasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null || user.IsDeleted)
            return Result.Failure(AuthErrors.UserNotFound);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(
            user,
            token,
            request.NewPassword
        );

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(new Error(
                "User.ChangePasswordFailed",
                result.Errors.First().Description,
                StatusCodes.Status400BadRequest
            ));
    }
}