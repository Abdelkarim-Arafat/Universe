
namespace Universe.Application.AuthServices.Commands.UpdateRefreshToken;

public record UpdateRefreshTokenCommand(
    string accessToken,
    string refreshToken
) : IRequest<Result<AuthResponse>>;