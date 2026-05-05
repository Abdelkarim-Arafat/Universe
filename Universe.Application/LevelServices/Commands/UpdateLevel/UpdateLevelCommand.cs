using Universe.Core.Contracts.Level;

namespace Universe.Application.LevelServices.Commands.UpdateLevel;

public record UpdateLevelCommand (
    [Required] Guid ProgramId,
    [Required] Guid Id,
    string Name,
    int MinHours,
    int MaxHours
) : IRequest<Result<LevelResponse>>;
