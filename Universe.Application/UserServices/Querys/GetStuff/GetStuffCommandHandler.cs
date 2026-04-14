using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetStuff;

public class GetStuffCommandHandler(
    UserManager<ApplicationUser> userManager
) : IRequestHandler<GetStuffCommand, Result<StuffWithDetailsResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<StuffWithDetailsResponse>> Handle(GetStuffCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId && !x.IsDeleted, cancellationToken);

        if (user is null)
            return Result.Failure<StuffWithDetailsResponse>(AuthErrors.UserNotFound);

        var roles = await _userManager.GetRolesAsync(user);

        return Result.Success(new StuffWithDetailsResponse(
            user.Id.ToString(),
            user.Name,
            roles.ToList(),
            user.UserName!,
            user.Email,
            user.PhoneNumber
        ));
    }
}