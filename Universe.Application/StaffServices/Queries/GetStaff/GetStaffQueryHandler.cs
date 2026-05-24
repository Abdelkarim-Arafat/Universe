using Universe.Core.Contracts.Staff;

namespace Universe.Application.StaffServices.Queries.GetStaff;

public class GetStaffQueryHandler(
    UserManager<ApplicationUser> userManager
) : IRequestHandler<GetStaffQuery, Result<StaffWithDetailsResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<StaffWithDetailsResponse>> Handle(GetStaffQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId && !x.IsDeleted, cancellationToken);

        if (user is null)
            return Result.Failure<StaffWithDetailsResponse>(AuthErrors.UserNotFound);

        var roles = await _userManager.GetRolesAsync(user);

        return Result.Success(new StaffWithDetailsResponse(
            user.Id.ToString(),
            user.Name,
            roles.ToList(),
            user.UserName!,
            user.Email,
            user.PhoneNumber
        ));
    }
}