using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.CourseOffering;
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Contracts.Student;
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
    Task<List<CourseOfferingAssessmentResponse>> GetCourseOfferingAssessmentsForViewAsync(Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<List<Guid>> GetStudentsIdsEnrolledInCourseAsync(Guid courseOfferingId, CancellationToken cancellationToken);
    Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<CourseOfferingData?> GetCourseOfferingDataByAssessmentIdAsync(Guid courseOfferingAssessmentId, CancellationToken cancellationToken);
}
