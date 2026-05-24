namespace Universe.Application.StudentServices.Commands.UpdateStudentGraduationDetails;

public record UpdateStudentGraduationDetailsCommand(
    [Required] Guid Id,
    [Required] Guid ProgramId,
    string GraduationYear,
    string GraduationSemester,
    string GraduationProjectName
) : IRequest<Result>;
