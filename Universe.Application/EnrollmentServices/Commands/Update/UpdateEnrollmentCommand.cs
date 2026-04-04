using Universe.Application.EnrollmentServices.Dtos;

namespace Universe.Application.EnrollmentServices.Commands.Update;

public record UpdateEnrollmentCommand
(
    [Required] Guid StudentId,
     NeededHours Hours,
     List<EnrollmentInfo> NewEnrollments
) : IRequest<Result<List<EnrollmentInfo>>>;


public record NeededHours
(
    int RegisterdHours,
    int MaxAllowedHours,
    int MinAllowedHours
);
