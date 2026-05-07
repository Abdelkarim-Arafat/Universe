using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Commands.UpdateService;

public record UpdateServiceCommand (
    [Required] Guid CollegeId,
    [Required] Guid Id,
    string Name,
    string Description,
    decimal Price
) : IRequest<Result<ServiceResponse>>;