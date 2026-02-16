

using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Commands.AddCoursePrerequisite;

public record AddCoursePreRequisiteCommand(
    [Required] Guid CourseId ,
    [Required] Guid PreRequisiteId
) : IRequest<Result<CourseResponse>>;
