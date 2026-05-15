using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.Level;

public record StudentStudyLoadDto
(
    string LevelName,
    int MinHours,
    int MaxHours
);
