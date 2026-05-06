using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Commands.Add_Service;

public record AddServiceCommand(
    [Required] Guid CollegeId,
    string Name,
    string Description,
    decimal Price
) : IRequest<Result<ServiceResponse>>;