using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public record DepartmentErrors
{
    public static readonly Error DepartmentAlreadyExists =
    new("Department.AlreadyExists", "Department name or code already exists", StatusCodes.Status409Conflict);

    public static readonly Error DepartmentNotFound = new(
        "Department.NotFound",
        "Department is not found",
        StatusCodes.Status404NotFound);
}
