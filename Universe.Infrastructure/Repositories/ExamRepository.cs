using Microsoft.EntityFrameworkCore;
using Universe.Core.Contracts.CourseOfferingExams;
using Universe.Core.Entities;
using Universe.Core.Enums;
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
       (Guid? Id, Guid SemesterId, Guid AcademicProgramId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        return await _context.ExamTerms.AnyAsync(e =>
           !e.IsDeleted
           && e.AcademicProgramId == AcademicProgramId
           && e.SemesterId == SemesterId
           && (((Id == null)) || (e.Id != Id))
           && (e.StartDate < endDate && startDate < e.EndDate), cancellationToken);
    }
    public async Task<bool> IsExistExamTermWithSameTypeAsync (Guid? Id, Guid SemesterId, Guid AcademicProgramId, ExamType examType, CancellationToken cancellationToken)
    {
        return await _context.ExamTerms.AnyAsync(e =>
           !e.IsDeleted
           && e.AcademicProgramId == AcademicProgramId
           && e.SemesterId == SemesterId
           && (((Id == null)) || (e.Id != Id))
           && examType == e.ExamType, cancellationToken);
    }

    public async Task<bool> IsExistExamTermAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _context.ExamTerms.AnyAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
    }

    public async Task<List<Guid>> GetCurrentExamTermIdsAsync(Guid programId, Guid semesterId, CancellationToken cancellationToken)
    {
        return await _context.ExamTerms
            .Where(exam => !exam.IsDeleted
                && exam.AcademicProgramId == programId
                && exam.SemesterId == semesterId
                && exam.IsPublished)
            .Select(exam => exam.Id)
            .ToListAsync(cancellationToken);
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
    #endregion

    #region CourseOfferingExam
    public async Task<CourseOfferingExam?> GetCourseOfferingExamByIdAsync
        (Guid Id, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingExams.FirstOrDefaultAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
    }
    public async Task<CourseOfferingExam?> GetCourseOfferingExamIncludingCommitteesAndSeatsAsync
        (Guid Id, CancellationToken cancellationToken)
    {
        return await _context.CourseOfferingExams
            .Include(coe => coe.CourseOfferingCommittees)
                .ThenInclude(coc => coc.ExamSeats)
                .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == Id && !e.IsDeleted, cancellationToken);
    }
    public async Task<bool> IsCourseOfferingExamExistAsync
        (Guid courseOfferingId, Guid examTermId, CancellationToken cancellationToken)
        => await _context.CourseOfferingExams.AnyAsync(coe =>
                                          !coe.IsDeleted
                                        && coe.CourseOfferingId == courseOfferingId
                                        && coe.ExamTermId == examTermId, cancellationToken);
    public async Task<bool> HasOverlappingExamAsync(
    Guid? courseOfferingExamId,
    Guid examTermId,
    List<Guid> examCommitteesIds,
    DateOnly date,  
    TimeOnly startTime,
    TimeOnly endTime,
    CancellationToken cancellationToken = default)
    {
        return await _context.CourseOfferingExams
            .AnyAsync(coe => !coe.IsDeleted
                          && ((courseOfferingExamId == null) || (coe.Id != courseOfferingExamId))
                          && coe.ExamTermId == examTermId
                          && coe.CourseOfferingCommittees
                               .Any(committee => examCommitteesIds.Contains(committee.ExamCommitteeId))
                          && coe.Date == date
                          && coe.StartTime < endTime
                          && startTime < coe.EndTime,
                      cancellationToken);
    }
    public async Task<List<ExamCommitteesDetails>?> GetCommitteesDetailsAsync
        (Guid examTermId, List<Guid> examCommitteesIds, CancellationToken cancellationToken)
    {
        if (examCommitteesIds == null || !examCommitteesIds.Any())
            return null;

        var examCommittees = await _context.ExamCommittees
            .Where(com => !com.IsDeleted && com.ExamTermId == examTermId && examCommitteesIds.Contains(com.Id))
            .Select(com => new ExamCommitteesDetails(com.Id, com.CommitteeNumber, com.MaxCapacity))
            .ToListAsync(cancellationToken);

        if (examCommitteesIds.Except(examCommittees.Select(c => c.Id)).Any())
            return null;

        return examCommittees;
    }
    #endregion

    #region CourseOfferingCommittees

    #endregion
}
