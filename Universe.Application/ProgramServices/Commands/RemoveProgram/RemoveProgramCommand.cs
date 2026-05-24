namespace Universe.Application.AcademicProgramServices.Commands.RemoveAcademicProgram;

public record RemoveAcademicProgramCommand(
    [Required] Guid CollegeId,
    [Required] Guid Id
) : IRequest<Result>;
