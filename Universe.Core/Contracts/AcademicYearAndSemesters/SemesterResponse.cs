using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.AcadimicYearAndSemesters;

public record SemesterResponse(
    Guid Id,
    TermType Name,
    DateOnly StartDate,
    DateOnly EndDate
);