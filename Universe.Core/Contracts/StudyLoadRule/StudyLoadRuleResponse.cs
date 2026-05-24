namespace Universe.Core.Contracts.StudyLoadRule;

public record StudyLoadRuleResponse(
    string Id,
    decimal GpaFrom,
    decimal GpaTo,
    int MinHours,
    int MaxHours
);
