using Universe.Application.LevelServices.Dtos;

namespace Universe.Application.LevelServices.Commands.Update;

public record UpdateLevelCommand
(
   [Required] Guid Id,
    string Name,
    int MinHours,
    int MaxHours
) : IRequest<Result<LevelResponse>>;
