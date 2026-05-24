using Universe.Core.Contracts.Staff;

namespace Universe.Application.StaffServices.Commands.UpdateStaff;

public class UpdateStaffCommandHandler(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager
) : IRequestHandler<UpdateStaffCommand, Result<StaffWithDetailsResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<Result<StaffWithDetailsResponse>> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<StaffWithDetailsResponse>(AuthErrors.UserNotFound);

        if (await _userManager.Users
            .AnyAsync(x => x.UserName == request.UserName && x.Id != request.UserId, cancellationToken))
            return Result.Failure<StaffWithDetailsResponse>(AuthErrors.DuplicateUserName);

        user.Name = request.Name;
        user.UserName = request.UserName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var error = updateResult.Errors.First();
            return Result.Failure<StaffWithDetailsResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        foreach (var role in request.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                return Result.Failure<StaffWithDetailsResponse>(AuthErrors.InvalidRoles);
        }

        var currentRoles = await _userManager.GetRolesAsync(user);

        var rolesToRemove = currentRoles.Except(request.Roles).ToList();
        var rolesToAdd = request.Roles.Except(currentRoles).ToList();

        if (rolesToRemove.Any())
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        if (rolesToAdd.Any())
            await _userManager.AddToRolesAsync(user, rolesToAdd);

        return Result.Success(new StaffWithDetailsResponse(
            user.Id.ToString(),
            user.Name,
            request.Roles,
            user.UserName,
            user.Email,
            user.PhoneNumber
        ));
    }
}