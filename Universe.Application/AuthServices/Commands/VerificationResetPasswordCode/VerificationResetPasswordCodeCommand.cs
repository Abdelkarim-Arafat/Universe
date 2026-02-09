using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AuthServices.Commands.VerificationResetPasswordCode;

public record VerificationResetPasswordCodeCommand(
    string Email,
    string Code
) : IRequest<Result>;