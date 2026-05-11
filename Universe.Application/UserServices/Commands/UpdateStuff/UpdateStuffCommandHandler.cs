using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.User;
using Universe.Core.Entities;

namespace Universe.Application.UserServices.Commands.UpdateStuff;

public class UpdateStuffCommandHandler(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager
) : IRequestHandler<UpdateStuffCommand, Result<StuffWithDetailsResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<Result<StuffWithDetailsResponse>> Handle(UpdateStuffCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<StuffWithDetailsResponse>(AuthErrors.UserNotFound);

        if (await _userManager.Users
            .AnyAsync(x => x.UserName == request.UserName && x.Id != request.UserId, cancellationToken))
            return Result.Failure<StuffWithDetailsResponse>(AuthErrors.DuplicateUserName);

        user.Name = request.Name;
        user.UserName = request.UserName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var error = updateResult.Errors.First();
            return Result.Failure<StuffWithDetailsResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        foreach (var role in request.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                return Result.Failure<StuffWithDetailsResponse>(AuthErrors.InvalidRoles);
        }

        var currentRoles = await _userManager.GetRolesAsync(user);

        var rolesToRemove = currentRoles.Except(request.Roles).ToList();
        var rolesToAdd = request.Roles.Except(currentRoles).ToList();

        if (rolesToRemove.Any())
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        if (rolesToAdd.Any())
            await _userManager.AddToRolesAsync(user, rolesToAdd);

        return Result.Success(new StuffWithDetailsResponse(
            user.Id.ToString(),
            user.Name,
            request.Roles,
            user.UserName,
            user.Email,
            user.PhoneNumber
        ));
    }
}