using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.AcademicEventServices.EvenetDtos;

public record EventResponse(
    EventType EventType,
    DateTime StartTime,
    DateTime EndTime
);
