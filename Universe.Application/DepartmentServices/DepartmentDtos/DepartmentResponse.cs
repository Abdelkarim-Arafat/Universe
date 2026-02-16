using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.DepartmentServices.DepartmentDtos;

public record DepartmentResponse(
    string Id,
    string Name,
    string Code,
    string Description,
    int RequiredCreditHours
);
