using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Level;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IStudyLoadByLevelRepository
{
    Task<StudyLoadByLevel?> GetByLevelIdAndSemesterIdAsync
        (Guid LevelId, Guid SemesterId, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(
        Guid programId,
        Guid levelId,
        Guid semesterId,
        CancellationToken cancellationToken);


    Task<StudyLoadByLevel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<StudyLoadByLevel>> GetAllStudyLoadByLevelAsync(Guid programId, CancellationToken cancellationToken);
    Task<StudentStudyLoadDto?> GetLevelStudyLoadAsync(Guid levelId, Guid semesterId, CancellationToken cancellationToken);
}
