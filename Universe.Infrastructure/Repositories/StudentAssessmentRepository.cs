using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class StudentAssessmentRepository(ApplicationDbContext context) : IStudentAssessmentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<StudentAssessment?> GetStudentAssessmentIncludingCourseAssessmentAsync
        (Guid studentId, Guid courseAssessmentId, CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments
             .Include(sa => sa.CourseOfferingAssessment)
             .Where(sa => sa.StudentId == studentId
                          && sa.CourseOfferingAssessmentId == courseAssessmentId
                          && !sa.IsDeleted)
             .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<decimal> GetStudentDegreeInCourseAsync
        (Guid studentId, Guid courseOfferingId, CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments
            .AsNoTracking()
            .Where(sa => sa.StudentId == studentId
                         && sa.CourseOfferingAssessment.CourseOfferingId == courseOfferingId
                         && !sa.IsDeleted)
            .SumAsync(sa => sa.degree ?? 0, cancellationToken);
    }
}
