
using Universe.Core.Enums;

namespace Universe.Core.Contracts.StudyLoadByLevel;

public record StudyLoadByLevelResponse(
    Guid Id,
    Guid LevelId,
    string LevelName,
    TermType SemesterName,
    int MinHours,
    int MaxHours
);
