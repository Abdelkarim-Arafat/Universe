 
namespace Universe.Application.AuthServices.Commands.Login;

public record LoginCommand(
    string UserName,
    string Password,
    bool RememberMe
) : IRequest<Result<AuthResponse>>;
