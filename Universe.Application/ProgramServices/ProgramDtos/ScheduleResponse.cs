using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.ProgramServices.ProgramDtos;

public record ScheduleResponse(
    TimeOnly DayStartTime,
    TimeOnly DayEndTime,
    int SlotDurationMinutes
);