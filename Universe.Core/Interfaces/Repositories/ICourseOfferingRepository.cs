using Universe.Core.Dtos.Enrollments;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ICourseOfferingRepository
{
    Task<bool> IsExistAsync(Guid AcademicProgramId, Guid SemesterId,
        Guid LevelId, Guid CourseId, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Guid CourseOfferingId, CancellationToken cancellationToken);

    Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments(Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, List<CourseOfferingAssessment>>>
        GetCourseOfferingsAssessmentsBulkAsync(List<Guid> CourseOfferingIds, CancellationToken cancellationToken);

    Task<decimal> RegistredHours(List<Guid> CourseOfferingIds, CancellationToken cancellationToken);
    Task<LevelRegistrationCatalogDto?> GetAvailableCoursesCatalogAsync(
       Guid studentId,
       Guid semesterId,
       Guid levelId,
       CancellationToken cancellationToken);
}
