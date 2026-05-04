using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.TeachingSession;

public record ScheduleResponse(
    TimeOnly DayStartTime,
    TimeOnly DayEndTime,
    int SlotDurationMinutes
);