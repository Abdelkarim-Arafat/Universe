namespace Universe.Application.ExamCommitteeServices.Commands.Delete;

public record DeleteExamCommitteeCommand
(
    [Required] Guid Id
) : IRequest<Result>;