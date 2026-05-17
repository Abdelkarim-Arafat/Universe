using Universe.Core.Contracts.StudyLoadByLevel;
using Universe.Core.Enums;

namespace Universe.Application.StudyLoadByLevelServices.Commands.AddStudyLoad;

public record AddStudyLoadByLevelCommand(
    [Required] Guid ProgramId,
    [Required] Guid LevelId,
    [Required] Guid AcademicYearId,
    TermType SemesterType,
    int MinHours,
    int MaxHours
) : IRequest<Result<StudyLoadByLevelResponse>>;
