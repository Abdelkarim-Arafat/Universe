
using Universe.Core.Contracts.CourseOfferings;
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ICourseOfferingRepository
{
    Task<bool> IsExistAsync(Guid AcademicProgramId, Guid SemesterId,
        Guid LevelId, Guid CourseId, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Guid CourseOfferingId, CancellationToken cancellationToken);

    Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments(Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<LevelRegistrationCatalogDto?> GetAvailableCoursesCatalogAsync(
       Guid studentId,
       Guid semesterId,
       Guid levelId,
       CancellationToken cancellationToken);
    Task<List<CourseOfferingAssessmentResponse>> GetCourseOfferingAssessmentsForViewAsync(Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<List<Guid>> getStudentsIdsByCourseOfferingIdAsync(Guid courseOfferingId, CancellationToken cancellationToken);
}
