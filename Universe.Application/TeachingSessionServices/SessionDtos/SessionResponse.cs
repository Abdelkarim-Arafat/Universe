using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.TeachingSessionServices.SessionDtos;


public record SessionResponse(
    string Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    SessionType Type,
    Core.Enums.DayOfWeek Day,
    string InstructorId,
    string InstructorName,
    string RoomId,
    string RoomName,
    int GroupNumber
);  