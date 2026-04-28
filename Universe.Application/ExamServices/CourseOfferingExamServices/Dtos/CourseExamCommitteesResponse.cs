namespace Universe.Application.ExamServices.CourseOfferingExamServices.Dtos;

public record CourseExamCommitteesResponse
(
    Guid Id,
    int CommitteeNumber,
    int Capacity,
    int NumberOfRegisteredStudents,
    int StartDistribution,
    string Place
);
