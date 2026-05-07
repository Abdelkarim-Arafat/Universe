using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.CourseOffering;
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Contracts.TeachingSession;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ICourseOfferingRepository
{
    Task<bool> IsExistAsync(Guid AcademicProgramId, Guid SemesterId, Guid LevelId, Guid CourseId, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<IReadOnlyList<CourseOfferingResponse>> GetLevelCoursesAsync(Guid LevelId, Guid SemesterId, CancellationToken cancellationToken);
    Task<IReadOnlyList<SessionResponse>> GetCourseOfferingSessionsAsync(Guid courseOfferingId, int GroupNumber, CancellationToken cancellationToken);
    Task<CourseOffering?> GetByIdIncludingAssessmentsAsync(Guid Id, CancellationToken cancellationToken);
    Task<LevelRegistrationCatalogDto?> GetAvailableCoursesCatalogAsync(
    Guid studentId,
    Guid semesterId,
    Guid levelId,
    CancellationToken cancellationToken);
    Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments(Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<List<CourseOffering>> GetAvailableCourseOfferingsIncludingCourseAsync(Guid LevelId, Guid SemesterId, Guid StudentId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, List<CourseOfferingAssessment>>> GetCourseOfferingsAssessmentsBulkAsync(List<Guid> CourseOfferingIds, CancellationToken cancellationToken);

    Task<int> CountCourseAssessments(List<Guid> CourseAssessmentsIds, CancellationToken cancellationToken);
    Task<decimal> RegistredHours(List<Guid> CourseOfferingIds, CancellationToken cancellationToken);
    Task<Dictionary<Guid, Guid>> CourseOfferingIdsToCourseIdAsync(List<Guid> CourseOfferingsIds, CancellationToken cancellationToken);
    Task<bool> IsOpenForControlAsync(Guid courseOfferingId, CancellationToken cancellationToken);
    Task<CourseOffering?> GetByIdIncludingEnrollmentsAsync(Guid courseOfferingId, CancellationToken cancellationToken);
    Task<Guid?> GetIdByCourseAssessmentIdAsync(Guid CourseAssessmentId, CancellationToken cancellationToken);
    Task<int> NumberOfRegisteredStudentsAsync(Guid CourseOfferingId,CancellationToken cancellationToken);
}
