using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IStudyLoadRuleRepository
{
    Task<bool> CheckOverLabedGpaAsync(Guid CollegeId, Guid? Id , decimal MinGpa, decimal MaxGpa, CancellationToken cancellationToken);
    Task<StudyLoadRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}