using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IStudyLoadByLevelRepository
{
    Task<bool> IsExistAsync(
        Guid programId,
        Guid levelId,
        Guid semesterId,
        CancellationToken cancellationToken);


    Task<StudyLoadByLevel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<StudyLoadByLevel>> GetAllStudyLoadByLevelAsync(Guid programId, CancellationToken cancellationToken);
}
