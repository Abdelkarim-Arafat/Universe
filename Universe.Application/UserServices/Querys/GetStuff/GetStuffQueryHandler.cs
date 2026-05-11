using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetStuff;

public class GetStuffQueryHandler(
    UserManager<ApplicationUser> userManager
) : IRequestHandler<GetStuffQuery, Result<StuffWithDetailsResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<StuffWithDetailsResponse>> Handle(GetStuffQuery request, CancellationToken cancellationToken)
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