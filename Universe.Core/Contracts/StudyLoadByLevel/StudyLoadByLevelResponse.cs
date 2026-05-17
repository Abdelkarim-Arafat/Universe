
using Universe.Core.Enums;

namespace Universe.Core.Contracts.StudyLoadByLevel;

public record StudyLoadByLevelResponse(
    Guid Id,
    TermType SemesterName,
    string LevelName,
    Guid LevelId,
    int MinHours,
    int MaxHours
);
