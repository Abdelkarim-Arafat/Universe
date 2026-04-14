using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.CourseServices.Dtos;

public record CourseWithPreRequisiteResponse(
    string Id,
    string Name,
    string Description,
    string Code,
    List<CourseResponse> PreRequisites
);
