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
    Task<ExamCommittee?> GetExamCommitteeByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsExistCommitteeWithSameNumberAsync
        (Guid? Id, Guid ExamTermId, int CommitteeNumber, CancellationToken cancellationToken);
    Task<Dictionary<Guid, int>> GetCommitteeCapacitiesLookupAsync
        (IEnumerable<Guid> ids, CancellationToken cancellationToken);
    #endregion

    #region CourseOfferingExam
    Task<CourseOfferingExam?> GetCourseOfferingExamByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<CourseOfferingExam?> GetCourseOfferingExamAsync
        (Guid courseOfferingId, Guid examTermId, CancellationToken cancellationToken);

    Task<bool> 
        IsCourseOfferingExamExistAsync
        (Guid courseOfferingId,Guid examTermId, CancellationToken cancellationToken);
    Task<List<Guid>> GetCourseOfferingsCommitteesIdsAsync(Guid courseOfferingExamId, CancellationToken cancellationToken);
    Task<int> CommitteesCapacitiesSumAsync(IEnumerable<Guid> committeesIds, CancellationToken cancellationToken);
    #endregion

    #region CourseOfferingCommitttees
    Task<List<CourseOfferingCommittee>> GetCourseOfferingCommitteesIncludingSeatsAsync
        (IEnumerable<Guid> committeesIds, CancellationToken cancellationToken);
    Task<bool> CheckOverLappedInCommitteesAsync
        (CourseOfferingExam courseOfferingExam, IEnumerable<Guid> commitsIds, CancellationToken cancellationToken);
    #endregion
}
