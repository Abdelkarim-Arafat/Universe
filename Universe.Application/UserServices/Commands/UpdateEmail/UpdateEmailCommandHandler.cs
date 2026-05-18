using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UpdateEmail;

public class UpdateEmailCommandHandler(
    UserManager<ApplicationUser> userManager
) : IRequestHandler<UpdateEmailCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<Result> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null || user.IsDeleted)
            return Result.Failure(AuthErrors.UserNotFound);

        var emailExists = await _userManager.Users
            .AnyAsync(x =>
                x.Email == request.Email &&
                x.Id != request.UserId,
                cancellationToken);

        if (emailExists)
            return Result.Failure(AuthErrors.DuplicatedEmail);

        user.Email = request.Email;
        user.NormalizedEmail = request.Email.ToUpper();

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(new Error(
                "User.UpdateEmailFailed",
                "Failed to update email",
                StatusCodes.Status400BadRequest
            ));
    }
}