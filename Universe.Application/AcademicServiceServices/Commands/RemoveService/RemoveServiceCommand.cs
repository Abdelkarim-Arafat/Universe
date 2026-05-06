using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Commands.RemoveService;

public record RemoveServiceCommand(
    [Required] Guid CollegeId,
    [Required] Guid Id
) : IRequest<Result>;