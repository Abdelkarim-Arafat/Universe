namespace Universe.Application.StaffServices.Commands.RemoveStaff;

public class RemoveStaffCommandHandler(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<RemoveStaffCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(RemoveStaffCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

        if (user is null) return Result.Failure(AuthErrors.UserNotFound);

        if(await _userManager.Users
            .AnyAsync(x => x.Id == request.Id && x.AdvisedStudents.Any(), cancellationToken)
            ) return Result.Failure(StaffErrors.StaffHasAdvisedStudents);

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}

