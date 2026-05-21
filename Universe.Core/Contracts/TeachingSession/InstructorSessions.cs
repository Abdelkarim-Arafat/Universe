using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Contracts.TeachingSession;

public record InstructorSessions (
    Guid Id,
    string CourseName,
    TimeOnly StartTime,
    TimeOnly EndTime,
    SessionType Type,
    Core.Enums.DayOfWeek Day,
    string RoomName,
    int GroupNumber
);
