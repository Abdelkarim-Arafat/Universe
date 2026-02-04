using MediatR;
using Universe.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AuthServices.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string Name
) : IRequest<Result>;
