namespace Universe.Application.ExamCommitteeServices.Dtos;

public record ExamCommitteeResponse
(
    Guid Id,
    Guid RoomId,
    Guid ExamTermId,
    int MaxCapacity,
    int CommitteeNumber
);