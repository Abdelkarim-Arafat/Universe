using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.AcadimicYearAndSemesters;

public record AcademicYearWithSemesterResponse(
    Guid Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    List<SemesterResponse> Semesters
);
