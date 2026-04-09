using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IStudyLoadByLevelRepository
{
    Task<StudyLoadByLevel?> GetByLevelIdAndSemesterIdAsync
        (Guid LevelId, Guid SemesterId, CancellationToken cancellationToken);
}
