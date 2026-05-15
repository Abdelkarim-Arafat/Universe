namespace Universe.Core.Contracts.ExamCommittees;

public record ExamCommitteeResponse
(
    Guid Id,
    int MaxCapacity,
    int CommitteeNumber,
    string Place
);