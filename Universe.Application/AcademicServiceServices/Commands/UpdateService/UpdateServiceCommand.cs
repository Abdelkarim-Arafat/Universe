using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicServiceServices.ServiceDtos;

namespace Universe.Application.AcademicServiceServices.Commands.UpdateService;

public record UpdateServiceCommand(
    [Required] Guid Id,
    string Name,
    string Description,
    decimal Price
) : IRequest<Result<ServiceResponse>>;