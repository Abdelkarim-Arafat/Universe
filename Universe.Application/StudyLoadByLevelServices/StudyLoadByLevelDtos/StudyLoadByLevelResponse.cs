using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.StudyLoadByLevelServices.StudyLoadByLevelDtos;

public record StudyLoadByLevelResponse(
    string Id,
    string LevelId,
    string LevelName,
    TermType SemesterName,
    int MinHours,
    int MaxHours
);
