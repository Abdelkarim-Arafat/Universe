using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Commands.UpdateDepartment;

public record UpdateDepartmentCommand(
    [Required] Guid Id,
    [Required] Guid CollegeId,
    string Name,
    string Description,
    string Code,
    int RequiredCreditHours
) : IRequest<Result<DepartmentResponse>>;