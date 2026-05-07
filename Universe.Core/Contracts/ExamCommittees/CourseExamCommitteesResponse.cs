namespace Universe.Core.Contracts.ExamCommittees;

public record CourseExamCommitteesResponse
(
    Guid Id,
    int CommitteeNumber,
    int Capacity,
    int NumberOfRegisteredStudents,
    int StartDistribution,
    string Place
);
