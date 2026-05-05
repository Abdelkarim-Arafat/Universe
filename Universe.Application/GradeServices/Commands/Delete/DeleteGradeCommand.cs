namespace Universe.Application.GradeServices.Commands.Delete;

public record DeleteGradeCommand
(
    Guid Id
) : IRequest<Result>;
