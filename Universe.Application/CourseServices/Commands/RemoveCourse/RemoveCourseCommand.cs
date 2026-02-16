using Microsoft.AspNetCore.Antiforgery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace Universe.Application.CourseServices.Commands.RemoveCourse;

public record RemoveCourseCommand(
    [Required] Guid Id
) : IRequest<Result>;
