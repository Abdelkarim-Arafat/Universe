using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.AcadimicYearAndSemestersServices.Dtos;

public record SemesterResponse(
    string Id,
    TermType Name,
    DateOnly StartDate,
    DateOnly EndDate
);