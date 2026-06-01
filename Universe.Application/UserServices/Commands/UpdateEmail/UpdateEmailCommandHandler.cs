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
        user.EmailConfirmed = true;
        user.NormalizedEmail = request.Email.ToUpper();

         await _userManager.UpdateAsync(user);

        return Result.Success();
    }
}