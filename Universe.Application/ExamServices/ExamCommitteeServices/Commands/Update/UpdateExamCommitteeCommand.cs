namespace Universe.Application.ExamCommitteeServices.Commands.Update;

public record UpdateExamCommitteeCommand
(
    int MaxCapacity,
    int CommitteeNumber,
    [Required] Guid Id
) : IRequest<Result>;