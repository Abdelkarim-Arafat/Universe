using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public class StaffErrors
{
    public static readonly Error StaffHasAdvisedStudents = new Error(
            "Staff.HasStudents",
            "This academic advisor cannot be removed because they are assigned to students.",
            StatusCodes.Status409Conflict
    );
}
