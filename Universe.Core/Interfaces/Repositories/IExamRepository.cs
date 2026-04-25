using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IExamRepository
{
    #region ExamTerms
    Task<ExamTerm?> GetExamTermByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsExistExamTermWithOverLabedTimeAsync
       (Guid? Id, Guid SemesterId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken);
    Task<bool> IsExistExamTermAsync(Guid Id, CancellationToken cancellationToken);
    #endregion

    #region ExamCommittees
    Task<bool> IsExistCommitteeWithSameNumberAsync
        (Guid CourseOfferingExamId, int CommitteeNumber, CancellationToken cancellationToken);
    #endregion

    #region CourseOfferingExam
    Task<CourseOfferingExam?> GetCourseOfferingExamByIdAsync(Guid Id, CancellationToken cancellationToken);
    #endregion
}
