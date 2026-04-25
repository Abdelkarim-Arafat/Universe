using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class ExamRepository
    (ApplicationDbContext context) : IExamRepository
{
    private readonly ApplicationDbContext _context = context;

    #region ExamTerms
    public async Task<ExamTerm?> GetExamTermByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _context.ExamTerms.FirstOrDefaultAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
    }

    public async Task<bool> IsExistExamTermWithOverLabedTimeAsync
       (Guid? Id, Guid SemesterId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        return await _context.ExamTerms.AnyAsync(e =>
           e.SemesterId == SemesterId
           && (((Id == null)) || (e.Id != Id))
           && (e.StartDate < endDate && startDate < e.EndDate), cancellationToken);
    }

    public async Task<bool> IsExistExamTermAsync(Guid Id,CancellationToken cancellationToken)
    {
        return await _context.ExamTerms.AnyAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
    }
    #endregion

    #region ExamCommittees
    public async Task<bool> IsExistCommitteeWithSameNumberAsync
        (Guid CourseOfferingExamId, int CommitteeNumber, CancellationToken cancellationToken)
    {
        return await _context.ExamCommittees.AnyAsync(e =>
           e.CourseOfferingExamId == CourseOfferingExamId
           && e.CommitteeNumber == CommitteeNumber, cancellationToken);
    }


    #endregion

    #region CourseOfferingExam
    public Task<CourseOfferingExam?> GetCourseOfferingExamByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return _context.CourseOfferingExams.FirstOrDefaultAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
    }
    #endregion
}
