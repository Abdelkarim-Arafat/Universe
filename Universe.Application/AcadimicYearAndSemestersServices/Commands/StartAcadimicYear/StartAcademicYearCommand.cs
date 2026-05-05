using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.AcadimicYearAndSemesters;
using Universe.Core.Enums;

namespace Universe.Application.AcademicYearAndSemestersServices.Commands.StartAcademicYear;

public  record StartAcademicYearCommand(
    [Required] Guid CollegeId,
    DateOnly StartDate,
    DateOnly EndDate,
    List<CreateSemesterDto> Semesters
) : IRequest<Result<AcademicYearWithSemesterResponse>>;

public record CreateSemesterDto(
    TermType TermType,
    DateOnly StartDate,
    DateOnly EndDate
);
