using Universe.Core.Contracts.AcadimicYearAndSemesters;
using Universe.Core.Enums;

namespace Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateAcademicYear;

public record UpdateAcademicYearCommand(
    [Required] Guid CollegeId,
    [Required] Guid Id,
    DateOnly StartDate,
    DateOnly EndDate,
    List<UpdateSemesterDto> Semesters
) : IRequest<Result<AcademicYearWithSemesterResponse>>;
public record UpdateSemesterDto(
    [Required] Guid Id,
    TermType TermType,
    DateOnly StartDate,
    DateOnly EndDate
);