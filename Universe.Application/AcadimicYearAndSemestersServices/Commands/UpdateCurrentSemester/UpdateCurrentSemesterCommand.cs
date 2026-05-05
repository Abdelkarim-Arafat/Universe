using Universe.Core.Contracts.AcadimicYearAndSemesters;
using Universe.Core.Enums;

namespace Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateCurrentSemester;

public record UpdateCurrentSemesterCommand (
    [Required] Guid AcademicYearId,
    [Required] TermType TermType
) : IRequest<Result<SemesterResponse>>;