
namespace Universe.Application.AuthServices.Commands.UpdateRefreshToken;

public record UpdateRefreshTokenCommand(
   string refreshToken
) : IRequest<Result<AuthResponse>>;