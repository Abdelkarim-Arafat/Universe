
namespace Universe.Application.AuthServices.Commands.RevokeRefreshToken;

public record RevokeRefreshTokenCommand(
    string accessToken,
    string refreshToken
) : IRequest<Result>;