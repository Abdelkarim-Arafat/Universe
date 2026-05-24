using Universe.Core.Contracts.StudyLoadByLevel;

namespace Universe.Application.StudyLoadByLevelServices.Commands.UpdateStudyLoad;

public record UpdateStudyLoadByLevelCommand(
    [Required] Guid ProgramId,
    [Required] Guid Id,
    int MinHours,
    int MaxHours
) : IRequest<Result<StudyLoadByLevelResponse>>;