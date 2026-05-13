namespace Universe.Application.ExamTermServices.Commands.Delete;

public record DeleteExamTermCommand
(
    [Required] Guid Id
) : IRequest<Result>;