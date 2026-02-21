
using Universe.Application.LevelServices.LevelDtos;

namespace Universe.Application.LevelServices.Commands.CreateLevel;

public record CreateLevelCommand
(
    Guid CollegeId,
    string Name,
    int MinHours,
    int MaxHours
) : IRequest<Result<LevelResponse>>;
