using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Commands.UpsertSchedule;

public record UpsertScheduleCommand(
    Guid ProgramId,
    Guid SemesterId,
    TimeOnly DayStartTime,
    TimeOnly DayEndTime,
    int SlotDurationMinutes
) : IRequest<Result<ScheduleResponse>>;