using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class StudyLoadByLevelRepository(
    ApplicationDbContext context
    ) : IStudyLoadByLevelRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<StudyLoadByLevel?> GetByLevelIdAndSemesterIdAsync
        (Guid LevelId, Guid SemesterId, CancellationToken cancellationToken)
    {
        return await _context.StudyLoadByLevels
            .FirstOrDefaultAsync(s => s.LevelId == LevelId && s.SemesterId == SemesterId, cancellationToken);
    }
}
