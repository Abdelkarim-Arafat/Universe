using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.TeachingSession;

public record InstructorSessions (
    string CourseName,
    SessionType Type,
    int GroupNumber,
    Core.Enums.DayOfWeek Day,
    TimeOnly Start,
    TimeOnly End,
    string RoomName
);
