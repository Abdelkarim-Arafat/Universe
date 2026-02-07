using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AuthServices.Commands.RevokeRefreshToken;

internal class UpdateRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
{
    public UpdateRefreshTokenCommandValidator()
    {
        RuleFor(x => x.accessToken)
            .NotEmpty().WithMessage("Access token is required.");
        RuleFor(x => x.refreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}
