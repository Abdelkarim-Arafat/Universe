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
    public async Task<bool> IsExistExamTermWithSameTypeAsync
      (Guid? Id, Guid SemesterId, Guid AcademicProgramId, ExamType examType, CancellationToken cancellationToken)
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

    public async Task<CourseExamCommitteesValidationDto> CreateCourseExamValidationAsync
        (DateOnly date, TimeOnly startTime, TimeOnly endTime,
        Guid courseOfferingId, Guid examTermId, List<Guid> examCommitteesIds, CancellationToken cancellationToken)
    {

        var isCourseOfferingExamExist = await _context.CourseOfferingExams
            .AnyAsync(coe => !coe.IsDeleted
                            && coe.CourseOfferingId == courseOfferingId
                            && coe.ExamTermId == examTermId, cancellationToken);

        var courseData = await _context.CourseOfferings
            .Where(co => co.Id == courseOfferingId && !co.IsDeleted)
            .Select(co => new
            {
                StudentIds = co.Enrollments
                    .Where(en => !en.IsDeleted)
                    .Select(en => en.StudentId)
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        bool isCourseOfferingExist = courseData != null;

        var studentsIds = courseData?.StudentIds ?? new List<Guid>();

        
        var isExamTermExist = await _context.ExamTerms
            .AnyAsync(et => et.Id == examTermId, cancellationToken);
        

        var isOverlappedTime = false;
        var examCommittees = new List<ExamCommitteesDetails>();

        if (examCommitteesIds != null && examCommitteesIds.Any())
        {
            isOverlappedTime = await _context.CourseOfferingExams
            .AnyAsync(coe => !coe.IsDeleted
                           && coe.CourseOfferingCommittees
                              .Any(committee => examCommitteesIds.Contains(committee.ExamCommitteeId))
                           && coe.Date == date
                           && coe.StartTime < endTime
                           && startTime < coe.EndTime,
                      cancellationToken);

            examCommittees = await _context.ExamCommittees
            .Where(com => !com.IsDeleted && com.ExamTermId == examTermId && examCommitteesIds.Contains(com.Id))
            .Select(com => new ExamCommitteesDetails(com.Id, com.CommitteeNumber, com.MaxCapacity))
            .ToListAsync(cancellationToken);
        }

        return new CourseExamCommitteesValidationDto(
            isCourseOfferingExamExist,
            isCourseOfferingExist,
            isExamTermExist,
            isOverlappedTime,
            examCommittees,
            studentsIds
        );
    }

    public async Task<UpdateCourseExamContextDto?> UpdateCourseExamContextAsync(
    DateOnly date, TimeOnly startTime, TimeOnly endTime,
    Guid courseOfferingExamId, List<Guid> examCommitteesIds, CancellationToken cancellationToken)
    {

        var exam = await _context.CourseOfferingExams
         .Include(coe => coe.CourseOfferingCommittees)
             .ThenInclude(coc => coc.ExamSeats)
         .AsSplitQuery()
         .FirstOrDefaultAsync(coe => coe.Id == courseOfferingExamId && !coe.IsDeleted, cancellationToken);

        if (exam == null)
            return null;

        var studentsIds = await _context.Enrollments
            .Where(en => !en.IsDeleted && en.CourseOfferingId == exam.CourseOfferingId)
            .Select(en => en.StudentId)
            .ToListAsync(cancellationToken);

        var isOverlappedTime = false;

        var examCommittees = new List<ExamCommitteesDetails>();

        if (examCommitteesIds != null && examCommitteesIds.Any())
        {
            isOverlappedTime = await _context.CourseOfferingExams
          .AnyAsync(coe => !coe.IsDeleted
                        && coe.Id != courseOfferingExamId
                        && coe.Date == date
                        && coe.StartTime < endTime
                        && startTime < coe.EndTime
                        && coe.CourseOfferingCommittees
                           .Any(committee => examCommitteesIds.Contains(committee.ExamCommitteeId)),
                    cancellationToken);

            examCommittees = await _context.ExamCommittees
              .AsNoTracking()
              .Where(com => !com.IsDeleted && examCommitteesIds.Contains(com.Id))
              .Select(com => new ExamCommitteesDetails(com.Id, com.CommitteeNumber, com.MaxCapacity))
              .ToListAsync(cancellationToken);
        }

        return new UpdateCourseExamContextDto(
            CourseOfferingExam: exam,
            isOverlappedTime: isOverlappedTime,
            examCommittees: examCommittees,
            studentsIds: studentsIds,
            committeesToRemove: exam.CourseOfferingCommittees.ToList()
        );
    }

    #endregion

    #region CourseOfferingCommittees

    #endregion
}
