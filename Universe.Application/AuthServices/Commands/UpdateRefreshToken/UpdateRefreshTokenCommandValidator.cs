
namespace Universe.Application.AuthServices.Commands.UpdateRefreshToken;

public class UpdateRefreshTokenCommandValidator : AbstractValidator<UpdateRefreshTokenCommand>
{
    public UpdateRefreshTokenCommandValidator()
    {
        RuleFor(x => x.accessToken)
            .NotEmpty().WithMessage("Access token is required.");
        RuleFor(x => x.refreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}
