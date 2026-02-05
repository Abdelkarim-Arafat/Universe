using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AuthServices.AuthDtos;
using Universe.Core.Abstractions;

namespace Universe.Application.AuthServices.Commands.Login;

public record LoginCommand(
    string UserName,
    string Password,
    bool RememberMe
) : IRequest<Result<AuthResponse>>;
