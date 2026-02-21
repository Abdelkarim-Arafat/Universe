 namespace Universe.Application.LevelServices.LevelDtos;

public record LevelResponse
(
    Guid Id,
    string Name,
    int MinHours,
    int MaxHours
);