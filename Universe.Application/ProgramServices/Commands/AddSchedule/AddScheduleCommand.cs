using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.ProgramServices.ProgramDtos;

namespace Universe.Application.ProgramServices.Commands.AddSchedule;

public record AddScheduleCommand (
    Guid ProgramId,
    Guid SemesterId,
    TimeOnly DayStartTime,
    TimeOnly DayEndTime,
    int SlotDurationMinutes
) : IRequest<Result<ScheduleResponse>>;