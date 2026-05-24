namespace Universe.Application.AcademicServiceServices.Commands.RemoveService;

public record RemoveServiceCommand(
    [Required] Guid CollegeId,
    [Required] Guid Id
) : IRequest<Result>;