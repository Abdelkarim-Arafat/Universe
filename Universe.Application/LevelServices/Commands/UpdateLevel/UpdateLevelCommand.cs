using Universe.Application.LevelServices.LevelDtos;

namespace Universe.Application.LevelServices.Commands.UpdateLevel;

public record UpdateLevelCommand
(
   [Required] Guid Id,
    string Name,
    int MinHours,
    int MaxHours
) : IRequest<Result<LevelResponse>>;
