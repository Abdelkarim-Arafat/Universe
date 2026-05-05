 namespace Universe.Core.Contracts.Level;

public record LevelResponse(
    Guid Id,
    string Name,
    int MinHours,
    int MaxHours
);