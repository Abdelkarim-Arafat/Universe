using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.CourseOffering;

public record CourseOfferingResponse(
    Guid Id,
    string Name,
    string Code,
    int NumberOfGroups
);
