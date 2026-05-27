using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Queries.GetImageUrl;

internal class GetImageUrlQueryHandler(
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<GetImageUrlQuery, Result<string?>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<string?>> Handle(GetImageUrlQuery request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId && !x.IsDeleted) is not { } user
            ) return Result.Failure<string?>(StudentErrors.UserNotFound);

        return Result.Success(user.ImageUrl);
    }
}
