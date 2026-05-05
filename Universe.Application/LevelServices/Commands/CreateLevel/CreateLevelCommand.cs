

using Universe.Core.Contracts.Level;

namespace Universe.Application.LevelServices.Commands.CreateLevel;

public record CreateLevelCommand
(
   [Required] Guid AcademicProgramId,
    string Name,
    int MinHours,
    int MaxHours
) : IRequest<Result<LevelResponse>>;
