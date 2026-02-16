using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Commands.AddDepartment;

public record AddDepartmentCommand(
    Guid CollegeId,
    string Name,
    string Code,
    string? Description,
    int RequiredCreditHours
) : IRequest<Result<DepartmentResponse>>;