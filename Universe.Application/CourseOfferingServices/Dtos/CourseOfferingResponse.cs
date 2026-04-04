using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.CourseOfferingServices.Dtos;

public record CourseOfferingResponse(
    string Id,
    string Name,
    string Code,
    int NumberOfGroups
);
