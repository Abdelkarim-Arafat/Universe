using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseOfferingServices.Dtos;

namespace Universe.Application.CourseOfferingServices.Query.GetCourseOffering;

public record GetCourseOfferingCommand (
    [Required] Guid Id
) : IRequest<Result<CourseOfferingWithDetailsResponse>>;