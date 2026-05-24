namespace Universe.Application.AcademicServiceRequestServices.Commands.AcceptServiceRequest;

public record AcceptServiceRequestCommand(
    [Required] Guid CollegeId,
    [Required] Guid RequestId
) : IRequest<Result>;