using Universe.Core.Contracts.Staff;

namespace Universe.Application.StaffServices.Commands.RegisterStaff;

public class RegisterStaffCommandHandler(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager
    ) : IRequestHandler<RegisterStaffCommand, Result<StaffWithDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<Result<StaffWithDetailsResponse>> Handle(RegisterStaffCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users
            .AnyAsync(x => x.UserName == request.UserName, cancellationToken))
            return Result.Failure<StaffWithDetailsResponse>(AuthErrors.DuplicateUserName);

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Name = request.Name,
            CollegeId = request.CollegeId,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<StaffWithDetailsResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        foreach (var role in request.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                return Result.Failure<StaffWithDetailsResponse>(AuthErrors.InvalidRoles);
        }

        await _userManager.AddToRolesAsync(user, request.Roles);

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
