using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.TeachingSessionServices.SessionDtos;
using Universe.Core.Enums;

namespace Universe.Application.TeachingSessionServices.Commands.AddSession;

public record AddSessionCommand(
    Guid CourseOfferingId,
    TimeOnly StartTime,
    TimeOnly EndTime,
    SessionType Type,
    Core.Enums.DayOfWeek Day,
    Guid InstructorId,
    Guid RoomId,
    int GroupNumber,
    int Capacity
) : IRequest<Result<SessionResponse>>;
