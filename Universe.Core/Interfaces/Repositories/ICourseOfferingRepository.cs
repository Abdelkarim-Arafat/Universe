using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ICourseOfferingRepository
{
    Task<bool> IsExistAsync(Guid AcademicProgramId, Guid SemesterId,
        Guid LevelId, Guid CourseId, CancellationToken cancellationToken);

    Task<CourseOffering?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<List<CourseOfferingAssessment>> GetCourseOfferingAssessments(Guid CourseOfferingId, CancellationToken cancellationToken);
    Task<List<CourseOffering>> GetCourseOfferingsByLevelAndSemesterIncludingCourseAsync(Guid LevelId, Guid SemesterId, CancellationToken cancellationToken);
    Task<Dictionary<Guid, List<CourseOfferingAssessment>>> GetCourseOfferingsAssessmentsBulkAsync(List<Guid> CourseOfferingIds, CancellationToken cancellationToken);
}
