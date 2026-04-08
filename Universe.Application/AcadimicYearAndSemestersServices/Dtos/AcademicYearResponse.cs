using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcadimicYearAndSemestersServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.AcademicYearAndSemestersServices.Dtos;

public record AcademicYearResponse(
    string Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    List<SemesterResponse> Semesters
);
