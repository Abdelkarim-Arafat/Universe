namespace Universe.Application.ExamServices.ExamTermServices.Commands.Delete;

public record DeleteExamTermCommand
(
    [Required] Guid Id
) : IRequest<Result>;