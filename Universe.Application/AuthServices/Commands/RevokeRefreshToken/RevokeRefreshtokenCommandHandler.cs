
namespace Universe.Application.AuthServices.Commands.RevokeRefreshToken;

internal class RevokeRefreshtokenCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider) : IRequestHandler<RevokeRefreshTokenCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if(_jwtProvider.ValidateToken(request.accessToken) is not { } userId)
            return Result.Failure(UserErrors.InvalidJwtToken);

        if (await _userManager.FindByIdAsync(userId) is not { } user)
            return Result.Failure(UserErrors.InvalidJwtToken);

        if(user.RefreshTokens.SingleOrDefault(rt => rt.Token == request.refreshToken && rt.IsActive) is not { } refreshToken)
            return Result.Failure(UserErrors.InvalidRefreshToken);


        refreshToken.RevokedOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }
}
