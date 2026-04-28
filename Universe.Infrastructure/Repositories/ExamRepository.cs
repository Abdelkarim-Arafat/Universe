using Microsoft.EntityFrameworkCore;
using System.Collections;
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

    public async Task<bool> IsExistExamTermAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _context.ExamTerms.AnyAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
    }
    #endregion

    #region ExamCommittees
    public async Task<ExamCommittee?> GetExamCommitteeByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _context.ExamCommittees
            .FirstOrDefaultAsync(com => com.Id == Id && !com.IsDeleted, cancellationToken);
    }
    public async Task<bool> IsExistCommitteeWithSameNumberAsync
        (Guid? Id, Guid ExamTermId, int CommitteeNumber, CancellationToken cancellationToken)
    {
        return await _context.ExamCommittees.AnyAsync(e =>
           !e.IsDeleted
           && ((Id == null) || (Id != e.Id))
           && e.ExamTermId == ExamTermId
           && e.CommitteeNumber == CommitteeNumber, cancellationToken);
    }

    public async Task<Dictionary<Guid,int>> GetCommitteeCapacitiesLookupAsync
        (IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return await _context.ExamCommittees
             .Where(com => !com.IsDeleted && ids.Contains(com.Id))
             .ToDictionaryAsync(com => com.Id, com => com.MaxCapacity);
    }
    public async Task<int> CommitteesCapacitiesSumAsync(IEnumerable<Guid> committeesIds, CancellationToken cancellationToken)
    {
        return await _context.ExamCommittees
            .Where(com => !com.IsDeleted && committeesIds.Contains(com.Id))
            .SumAsync(com => com.MaxCapacity);
    }


    #endregion

    #region CourseOfferingExam
    public async Task<CourseOfferingExam?> GetCourseOfferingExamByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingExams.FirstOrDefaultAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
    }
    public async Task<CourseOfferingExam?> GetCourseOfferingExamAsync
        (Guid courseOfferingId, Guid examTermId, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingExams
            .FirstOrDefaultAsync(courseExam =>
            !courseExam.IsDeleted
            && courseExam.CourseOfferingId == courseOfferingId
            && courseExam.ExamTermId == examTermId);
    }



    public async Task<bool>
       IsCourseOfferingExamExistAsync
       (Guid courseOfferingId, Guid examTermId, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingExams
            .AnyAsync(
               courseExam => !courseExam.IsDeleted
            && courseExam.CourseOfferingId == courseOfferingId
            && courseExam.ExamTermId == examTermId);
    }
    #endregion

    #region CourseOfferingCommittees

    public async Task<List<CourseOfferingCommittee>> GetCourseOfferingCommitteesIncludingSeatsAsync
        (IEnumerable<Guid> committeesIds, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingCommittees
            .Include(com => com.ExamSeats)
            .Where(com => !com.IsDeleted && committeesIds.Contains(com.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> CheckOverLappedInCommitteesAsync
        (CourseOfferingExam courseOfferingExam, IEnumerable<Guid> commitsIds, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingCommittees
            .AnyAsync(com => 
              !com.IsDeleted
            && com.CourseOfferingExamId != courseOfferingExam.Id
            && commitsIds.Contains(com.ExamCommitteeId)
            && courseOfferingExam.Date == com.CourseOfferingExam.Date
            && com.CourseOfferingExam.StartTime < courseOfferingExam.EndTime
            && courseOfferingExam.StartTime < com.CourseOfferingExam.EndTime,
            cancellationToken);
    }
    public async Task<List<Guid>> GetCourseOfferingsCommitteesIdsAsync(Guid courseOfferingExamId, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingCommittees
            .Where(com => com.CourseOfferingExamId == courseOfferingExamId)
            .Select(com => com.ExamCommitteeId)
            .ToListAsync(cancellationToken);
    }
    #endregion
}
