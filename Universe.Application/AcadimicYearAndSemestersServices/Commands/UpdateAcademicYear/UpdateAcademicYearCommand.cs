using Universe.Application.AcademicYearAndSemestersServices.Dtos;
using Universe.Core.Enums;

namespace Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateAcademicYear;

public record UpdateAcademicYearCommand(
    [Required] Guid CollegeId,
    [Required] Guid Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    List<UpdateSemesterDto> Semesters
) : IRequest<Result<AcademicYearResponse>>;
public record UpdateSemesterDto(
    [Required] Guid Id,
    TermType TermType,
    DateOnly StartDate,
    DateOnly EndDate
);