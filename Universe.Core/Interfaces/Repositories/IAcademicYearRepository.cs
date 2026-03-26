using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Core.Interfaces.Repositories;

public interface IAcademicYearRepository
{
    Task<AcademicYear?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> IsExistSemesterAsync(Guid Id, CancellationToken cancellationToken);
    Task<AcademicYear?> GetByIdWithSemestersAsync(Guid Id, CancellationToken cancellationToken);
    Task<AcademicYear?> GetCurrentYearAsync(Guid collegeId, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Guid Id, CancellationToken cancellationToken);
    Task<Semester?> GetCurrentSemesterAsync(Guid academicYearId, CancellationToken cancellationToken);
    Task<Semester?> GetSemesterByTypeAsync(Guid academicYearId, TermType type, CancellationToken cancellationToken);
    Task<bool> IsMakeConflictAsync(Guid CollegeId, string Name, DateOnly start, DateOnly end, Guid? Id, CancellationToken cancellationToken);
}
