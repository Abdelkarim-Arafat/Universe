
using Microsoft.AspNetCore.Identity.Data;

namespace Universe.Application.AuthServices.Commands.RevokeRefreshToken;

public class RevokeRefreshtokenCommandHandler(
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<RevokeRefreshTokenCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == request.refreshToken), cancellationToken);

        if (user is null)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidRefreshToken);

        var refreshToken = user.RefreshTokens
            .OrderByDescending(x => x.CreatedOn)
            .First(rt => rt.Token == request.refreshToken);

        refreshToken.RevokedOn = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        return Result.Success();
    }
}
