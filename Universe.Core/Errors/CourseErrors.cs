using Microsoft.AspNetCore.Http;
using Universe.Core.Abstractions;

namespace Universe.Core.Errors;

public record CourseErrors
{
    public static readonly Error CourseAlreadyExists =
        new("Course.AlreadyExists", "Course name or code already exists", StatusCodes.Status409Conflict);

    public static readonly Error PrerequisiteAlreadyExists =
    new("CoursePreRequisite.AlreadyExists",
        "This prerequisite relationship already exists." , StatusCodes.Status409Conflict);

    public static readonly Error PrerequisiteCycleDetected =
    new("CoursePreRequisite.CycleDetected",
        "Adding this prerequisite would create a cycle.",
        StatusCodes.Status409Conflict);

    public static readonly Error PrerequisiteNotFound =
    new("CoursePrerequisite.NotFound",
        "The prerequisite relationship does not exist.",
        StatusCodes.Status404NotFound);

    public static readonly Error SameCourse =
        new("CoursePreRequisite.SameCourse", "A course cannot be a prerequisite of itself." , StatusCodes.Status409Conflict);

    public static readonly Error CourseNotFound = new(
        "Course.NotFound",
        "Course is not found",
        StatusCodes.Status404NotFound);
}
