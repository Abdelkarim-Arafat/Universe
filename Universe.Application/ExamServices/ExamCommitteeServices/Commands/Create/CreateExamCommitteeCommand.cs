using Universe.Application.ExamCommitteeServices.Dtos;

namespace Universe.Application.ExamCommitteeServices.Commands.Create;

public record CreateExamCommitteeCommand
(
    int MaxCapacity,
    int CommitteeNumber,
    Guid ExamTermId,
    Guid RoomId
) : IRequest<Result<ExamCommitteeResponse>>;