using Universe.Core.Contracts.CourseOfferingExams;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Core.Interfaces.Repositories;

public interface IExamRepository
{
    #region ExamTerms
    Task<ExamTerm?> GetExamTermByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsExistExamTermWithOverLabedTimeAsync
       (Guid? Id, Guid SemesterId,Guid AcademicProgramId, DateOnly startDate,
        DateOnly endDate, CancellationToken cancellationToken);
    Task<bool> IsExistExamTermAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsExistExamTermWithSameTypeAsync
        (Guid? Id, Guid SemesterId, Guid AcademicProgramId, ExamType examType, CancellationToken cancellationToken);

    #endregion

    #region ExamCommittees
    Task<ExamCommittee?> GetExamCommitteeByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsExistCommitteeWithSameNumberAsync
        (Guid? Id, Guid ExamTermId, int CommitteeNumber, CancellationToken cancellationToken);
    #endregion

    #region CourseOfferingExam
    Task<CourseOfferingExam?> GetCourseOfferingExamByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<List<ExamCommitteesDetails>?> GetCommitteesDetailsAsync
    (Guid examTermId, List<Guid> examCommitteesIds, CancellationToken cancellationToken);

    Task<UpdateCourseExamContextDto?> 
        UpdateCourseExamContextAsync
        (DateOnly date, TimeOnly startTime, TimeOnly endTime,
        Guid courseOfferingExamId, List<Guid> examCommitteesIds, CancellationToken cancellationToken);
    Task<bool> HasOverlappingExamAsync(Guid examTermId, List<Guid> examCommitteesIds, DateOnly date, TimeOnly startTime, TimeOnly endTime, CancellationToken cancellationToken = default);
    Task<bool> IsCourseOfferingExamExistAsync(Guid courseOfferingId, Guid examTermId, CancellationToken cancellationToken);

    #endregion

    #region CourseOfferingCommitttees
    #endregion
}
