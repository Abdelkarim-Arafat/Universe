using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.CourseServices.Dtos;

public record CourseResponse(
    string Id,
    string Name,
    string Description,
    string Code,
    List<CourseResponse> PreRequisites
);
