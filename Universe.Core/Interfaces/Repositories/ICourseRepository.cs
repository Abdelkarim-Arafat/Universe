using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface ICourseRepository
{
    Task<bool> IsExistAsync(Guid collegeId, string name, string code, Guid? excludeId, CancellationToken cancellationToken);
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IList<Guid>> ExistingPreRequisitesIdsAsync(List<Guid> preRequisitesIds, CancellationToken cancellationToken);
    Task<IEnumerable<Course>> GetAllAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsExistCoursePreRequisiteAsync(Guid courseId, Guid preRequisiteId, CancellationToken cancellationToken);
    Task<IEnumerable<Course>> GetAllPreRequisiteAsync(Guid courseId, CancellationToken cancellationToken);
    Task<CoursePrerequisite?> GetCoursePreRequisiteAsync(Guid courseId, Guid PreRequisiteId, CancellationToken cancellationToken);
    Task RemovePrerequisiteAsync(Guid courseId , Guid preReqId,CancellationToken cancellationToken);
    Task<IList<Guid>> GetDirectPreRequisitesIdsAsync(Guid courseId, CancellationToken cancellationToken);
}
