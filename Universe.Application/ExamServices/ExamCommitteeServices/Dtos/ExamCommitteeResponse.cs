namespace Universe.Application.ExamCommitteeServices.Dtos;

public record ExamCommitteeResponse
(
    Guid Id,
    int MaxCapacity,
    int CommitteeNumber,
    string Place
);