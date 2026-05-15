using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.User;

public record GraduationDetailsResponse(
    decimal GPA,
    string GraduationYear,
    string GraduationSemester,
    string GraduationProjectName
);