namespace Universe.Core.Contracts.CourseOffering;

public record CourseOfferingCustomDto(
    bool IsCourseOpenForControl,
    decimal SuccessPercentage,
    Guid CourseOfferingId,
    Guid SemesterId
);
