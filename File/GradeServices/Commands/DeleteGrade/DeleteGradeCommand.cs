namespace Universe.Application.GradeServices.Commands.DeleteGrade;

public record DeleteGradeCommand
(
    Guid Id
) : IRequest<Result>;
