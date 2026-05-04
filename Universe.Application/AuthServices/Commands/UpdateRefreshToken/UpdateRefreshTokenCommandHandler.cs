
namespace Universe.Application.AuthServices.Commands.UpdateRefreshToken;

public class UpdateRefreshTokenCommandHandler(
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider,
    UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateRefreshTokenCommand, Result<AuthResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<AuthResponse>> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == request.refreshToken), cancellationToken);

        if (user is null)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidRefreshToken);

        var refreshToken = user.RefreshTokens
            .OrderByDescending(x => x.CreatedOn)
            .First(rt => rt.Token == request.refreshToken);

        if(refreshToken.IsExpired)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidRefreshToken);

        if (user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthResponse>(AuthErrors.LockedUser);

        refreshToken.RevokedOn = DateTime.UtcNow;

        var userRoles = await _userManager.GetRolesAsync(user);
        var userPermissions = await _unitOfWork.RoleRepository.GetUserPermissionsAsync(userRoles , cancellationToken);

        var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);

        var newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refreshTokenExpiration = refreshToken.ExpiresOn.AddMinutes(10);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);

        var response = new AuthResponse(
            user.Id.ToString(),
            user.CollegeId.ToString(),
            user.Name,
            user.ImageUrl,
            user.Email,
            userRoles,
            userPermissions,
            newToken,
            expiresIn,
            newRefreshToken,
            refreshTokenExpiration);

        return Result.Success(response);
    }
}