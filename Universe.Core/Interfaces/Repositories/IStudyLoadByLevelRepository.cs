using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Level;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Core.Interfaces.Repositories;

public interface IStudyLoadByLevelRepository
{

    Task<StudyLoadByLevel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<StudyLoadByLevel>> GetAllStudyLoadByLevelAsync(Guid programId, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Guid programId, Guid levelId, TermType semesterType, CancellationToken cancellationToken);
    Task<StudentStudyLoadDto?> GetLevelStudyLoadAsync(Guid levelId, TermType semesterType, CancellationToken cancellationToken);
}
