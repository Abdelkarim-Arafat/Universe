using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;

namespace Universe.Application.UserServices.Commands.RegisterStaff;

public class RegisterStaffCommandHandler(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager
    ) : IRequestHandler<RegisterStaffCommand , Result<StaffResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<Result<StaffResponse>> Handle(RegisterStaffCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users
            .AnyAsync(x => x.UserName == request.UserName, cancellationToken)
            ) return Result.Failure<StaffResponse>(AuthErrors.DuplicateUserName);

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Name = request.Name,
            CollegeId = request.CollegeId
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<StaffResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        if (!await _roleManager.RoleExistsAsync(request.Role))
            return Result.Failure<StaffResponse>(AuthErrors.InvalidRoles);

        await _userManager.AddToRoleAsync(user, request.Role);
        return Result.Success(new StaffResponse(user.Id.ToString(), user.Name, request.Role , user.UserName));
    }
}
