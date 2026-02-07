
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
        if (_jwtProvider.ValidateToken(request.accessToken) is not { } userId)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidJwtToken);

        if (await _userManager.FindByIdAsync(userId) is not { } user)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidJwtToken);

        if(user.IsDeleted) 
            return Result.Failure<AuthResponse>(AuthErrors.DisabledUser);

        if(user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthResponse>(AuthErrors.LockedUser);

        if(user.RefreshTokens.SingleOrDefault(x => x.Token == request.refreshToken && x.IsActive) is not { } userRefreshToken)
            return Result.Failure<AuthResponse>(AuthErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var userRoles = await _userManager.GetRolesAsync(user);
        var userPermissions = await _unitOfWork.RoleRepository.GetUserPermissionsAsync(userRoles , cancellationToken);

        var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);

        var newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);

        var response = new AuthResponse(
            user.Id.ToString(),
            user.Name,
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