using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.TeachingSessionServices.SessionDtos;


public record SessionResponse(
    Guid Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    SessionType Type,
    Core.Enums.DayOfWeek Day,
    Guid InstructorId,
    Guid RoomId,
    int GroupNumber
);  