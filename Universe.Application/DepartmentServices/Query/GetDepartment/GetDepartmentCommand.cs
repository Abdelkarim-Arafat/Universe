using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Query.GetDepartment;

public record GetDepartmentCommand(
    [Required] Guid Id
) : IRequest<Result<DepartmentResponse>>;
