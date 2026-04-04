using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.TeachingSessionServices.SessionDtos;

namespace Universe.Application.TeachingSessionServices.Commands.UpsertSchedule;

public record UpsertScheduleCommand(
    Guid ProgramId,
    Guid SemesterId,
    TimeOnly DayStartTime,
    TimeOnly DayEndTime,
    int SlotDurationMinutes
) : IRequest<Result<ScheduleResponse>>;