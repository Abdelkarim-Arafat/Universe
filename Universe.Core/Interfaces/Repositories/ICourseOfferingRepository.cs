using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ICourseOfferingRepository
{
    Task<bool> IsExistAsync(Guid AcademicProgramId, Guid SemesterId,
        Guid LevelId, Guid CourseId, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Guid CourseOfferingId, CancellationToken cancellationToken);

    Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments(Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<List<CourseOffering>> GetAvailableCourseOfferingsAsync(Guid LevelId, Guid SemesterId, Guid StudentId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, List<CourseOfferingAssessment>>>
        GetCourseOfferingsAssessmentsBulkAsync(List<Guid> CourseOfferingIds, CancellationToken cancellationToken);

    Task<int> CountCourseAssessments(List<Guid> CourseAssessmentsIds, CancellationToken cancellationToken);
    Task<decimal> RegistredHours(List<Guid> CourseOfferingIds, CancellationToken cancellationToken);
    Task<Dictionary<Guid, Guid>>
        CourseOfferingIdsToCourseIdAsync(List<Guid> CourseOfferingsIds, CancellationToken cancellationToken);
    Task<bool> IsOpenForControlAsync(Guid courseOfferingId, CancellationToken cancellationToken);
    Task<CourseOffering?> GetByIdIncludingEnrollmentsAsync(Guid courseOfferingId, CancellationToken cancellationToken);
    Task<Guid?> GetIdByCourseAssessmentIdAsync(Guid CourseAssessmentId, CancellationToken cancellationToken);
}
