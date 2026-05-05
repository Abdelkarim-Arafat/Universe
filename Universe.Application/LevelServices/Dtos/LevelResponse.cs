 namespace Universe.Application.LevelServices.Dtos;

public record LevelResponse
(
    Guid Id,
    string Name,
    int MinHours,
    int MaxHours
);