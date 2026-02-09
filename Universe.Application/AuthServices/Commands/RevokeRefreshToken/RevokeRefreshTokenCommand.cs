
namespace Universe.Application.AuthServices.Commands.RevokeRefreshToken;

public record RevokeRefreshTokenCommand(
    string refreshToken
) : IRequest<Result>;