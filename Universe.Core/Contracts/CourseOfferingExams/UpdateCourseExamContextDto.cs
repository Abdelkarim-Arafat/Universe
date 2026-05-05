using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Contracts.CourseOfferingExams;

public record UpdateCourseExamContextDto
(
    CourseOfferingExam CourseOfferingExam,
    bool isOverlappedTime,
    List<ExamCommitteesDetails> examCommittees,
    List<Guid> studentsIds,
    List<CourseOfferingCommittee> committeesToRemove
);