namespace Universe.Core.Contracts.CourseOfferingExams;

public record CourseExamCommitteesValidationDto
(
    bool isCourseOfferingExamExist,
    bool isCourseOfferingExist,
    bool isExamTermExist,
    bool isOverlappedTime,
    List<ExamCommitteesDetails> examCommittees,
    List<Guid> studentsIds
);
public record ExamCommitteesDetails
(
    Guid Id,
    int CommitteeNumber,
    int Capacity
);
