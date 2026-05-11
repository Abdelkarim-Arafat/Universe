namespace Universe.Application.CourseOfferingExamServices.Dtos;

public record CourseOfferingExamResponse
(
     Guid Id,
     DateOnly Date,
     TimeOnly StartTime,
     TimeOnly EndTime,
     PaginationList<CourseExamCommittees> CourseCommittees
);

public record CourseExamCommittees
(
    Guid Id,
    int CommitteeNumber,
    int Capacity,
    int NumberOfRegisteredStudents,
    int StartDistribution,
    string Place
);