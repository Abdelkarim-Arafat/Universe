using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Contracts.Student;
using Universe.Core.Enums;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;
namespace Universe.Infrastructure.Repositories;

public class EnrollmentRepository(
    ApplicationDbContext context
    ) : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<StudentAcademicHistoryContextDto?> GetStudentAcademicHistoryContextAsync(
      Guid studentId,
      CancellationToken cancellationToken)
    {
        var studentProgramId = await _context.Students
            .AsNoTracking()
            .Where(s => s.Id == studentId && !s.IsDeleted)
            .SelectMany(s => s.StudentAcademicPrograms)
            .Where(sap => sap.Currently)
            .Select(sap => sap.AcademicProgramId)
            .FirstOrDefaultAsync(cancellationToken);

        var rawEnrollments = await _context.Enrollments
            .AsNoTracking()
            .Where(e => e.StudentId == studentId && !e.IsDeleted)
            .Select(e => new
            {
                SemesterId = e.CourseOffering.Semester.Id,
                SemesterName = e.CourseOffering.Semester.Name.ToString(),
                AcademicYearName = e.CourseOffering.Semester.AcademicYear.Name,
                SemesterStartDate = e.CourseOffering.Semester.StartDate,
                AcademicYearStartDate = e.CourseOffering.Semester.AcademicYear.StartDate,

                CourseOfferingId = e.CourseOfferingId,
                CourseCode = e.CourseOffering.Course.Code,
                CourseName = e.CourseOffering.Course.Name,
                CreditHours = e.CourseOffering.CreditHours,
                Status = e.Status,

                TotalDegree = _context.StudentAssessments
                    .Where(sa => sa.StudentId == studentId
                              && sa.CourseOfferingAssessment.CourseOfferingId == e.CourseOfferingId
                              && !sa.IsDeleted)
                    .Sum(sa => sa.degree ?? 0)
            })
            .ToListAsync(cancellationToken);


        var semesterRecords = rawEnrollments
            .GroupBy(e => new { e.SemesterId, e.SemesterName, e.AcademicYearName, e.SemesterStartDate, e.AcademicYearStartDate })
            .OrderBy(g => g.Key.AcademicYearStartDate)
            .ThenBy(g => g.Key.SemesterStartDate)
            .Select(g => new StudentSemesterRecord(
                g.Key.SemesterName,
                g.Key.AcademicYearName,
                g.Key.SemesterStartDate,
                g.Select(e => new CourseRecord(
                    e.CourseOfferingId,
                    e.CourseCode,
                    e.CourseName,
                    e.CreditHours,
                    e.TotalDegree,
                    e.Status == EnrollmentStatus.Passed
                )).ToList()
            )).ToList();

        return new StudentAcademicHistoryContextDto(studentProgramId, semesterRecords);
    }
    public async Task<List<StudentExistingEnrollment>> GetStudentScheduleAsync
        (Guid studentId, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var currentYearId = await _context.AcademicYears
            .Where(ay => !ay.IsDeleted &&
                         today >= ay.StartDate &&
                         today <= ay.EndDate)
            .Select(ay => ay.Id)
            .FirstOrDefaultAsync(cancellationToken);

        var currentSemesterId = await _context.Semesters
            .Where(s => s.AcademicYearId == currentYearId && s.IsCurrent && !s.IsDeleted)
            .Select(s => s.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return await _context.TeachingSessionEnrollments
            .AsNoTracking()
            .Where(te => te.Enrollment.StudentId == studentId
                      && te.Enrollment.CourseOffering.SemesterId == currentSemesterId
                      && !te.IsDeleted)
            .Select(te => new StudentExistingEnrollment(
                te.TeachingSessionId,
                te.Enrollment.CourseOfferingId,
                te.Enrollment.CourseOffering.Course.Name,
                te.TeachingSession.Instructor.Name,
                te.TeachingSession.Room.Building.Name,
                te.TeachingSession.Room.RoomNumber,
                te.Enrollment.GroupNumber,
                te.TeachingSession.Type,
                te.TeachingSession.StartTime,
                te.TeachingSession.EndTime,
                te.TeachingSession.Day
            ))
            .ToListAsync(cancellationToken);
    }
    // my own
    public async Task<UpdateEnrollmentValidationDto?> GetUpdateEnrollmentValidationDataAsync(
     Guid studentId,
     Guid semesterId,
     List<Guid> courseOfferingIds,
     List<Guid> sessionIds,
     CancellationToken cancellationToken)
    {
        return await _context.Students
            .AsNoTracking()
            .Where(s => s.Id == studentId && !s.IsDeleted)
            .Select(s => new
            {
                TotalEarnedHours = s.Enrollments
                    .Where(e => e.Status == EnrollmentStatus.Passed && !e.IsDeleted)
                    .Sum(e => (decimal?)e.CourseOffering.CreditHours) ?? 0,
                Student = s,
                CurrentProgramId = s.StudentAcademicPrograms
                    .Where(sap => sap.Currently)
                    .Select(sap => sap.AcademicProgramId)
                    .FirstOrDefault()
            })
            .Select(temp => new
            {
                BaseData = temp,
                StudyLoad = _context.StudyLoadByLevels
                    .Where(sl => sl.Level.AcademicProgramId == temp.CurrentProgramId
                         && temp.TotalEarnedHours >= sl.Level.MinHours
                         && temp.TotalEarnedHours <= sl.Level.MaxHours
                         && sl.SemesterId == semesterId)
                    .Select(sl => new { sl.MinHours, sl.MaxHours })
                    .FirstOrDefault()
            })
            .Select(temp => new UpdateEnrollmentValidationDto(
                _context.Semesters.Any(sem => sem.Id == semesterId && !sem.IsDeleted),
                temp.StudyLoad.MinHours,
                temp.StudyLoad.MaxHours,
                _context.CourseOfferings
                       .Where(co => courseOfferingIds.Contains(co.Id) && !co.IsDeleted)
                       .Sum(co => co.CreditHours),
                _context.CourseOfferingSessions
                       .Where(cos => sessionIds.Contains(cos.TeachingSessionId) && !cos.IsDeleted)
                       .Select(cos => new SessionDetailsDto
                       (
                           cos.TeachingSessionId,
                           cos.CourseOfferingId,
                           cos.TeachingSession.Day,
                           cos.TeachingSession.StartTime,
                           cos.TeachingSession.EndTime,
                           cos.TeachingSession.Type,
                           cos.TeachingSession.GroupNumber,
                           cos.TeachingSession.Capacity,
                           cos.TeachingSession.TeachingSessionEnrollments.Count(ts => !ts.IsDeleted)
                       )).ToList()
                ))
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<EnrollmentExecutionContextDto> GetEnrollmentExecutionDataAsync(
    Guid studentId,
    Guid semesterId,
    HashSet<Guid> incomingCourseOfferingIds,
    CancellationToken cancellationToken)
    {
        var existingEnrollments = await _context.Enrollments
            .Include(e => e.TeachingSessionEnrollments.Where(ts => !ts.IsDeleted))
            .Where(e => !e.IsDeleted
                 && e.StudentId == studentId
                 && e.Status == EnrollmentStatus.InProgress
                 && e.CourseOffering.SemesterId == semesterId)
            .ToListAsync(cancellationToken);

        var incomingAssessmentsLookup = (await _context.CourseOfferingAssessments
            .Where(a => incomingCourseOfferingIds.Contains(a.CourseOfferingId) && !a.IsDeleted)
            .Select(a => new { a.CourseOfferingId, a.Id })
            .ToListAsync(cancellationToken))
            .ToLookup(a => a.CourseOfferingId, a => a.Id);

        return new EnrollmentExecutionContextDto(existingEnrollments, incomingAssessmentsLookup);
    }
    // يتم المراجعه
    public async Task<EnrollmentValidationContextDto?> GetEnrollmentValidationContextAsync(
    Guid studentId,
    Guid semesterId,
    CancellationToken cancellationToken)
    {
        return await _context.Students
            .AsNoTracking() 
            .Where(s => s.Id == studentId && !s.IsDeleted)
            .Select(s => new {
                Student = s,
                CurrentProgramId = s.StudentAcademicPrograms
                    .Where(sap => sap.Currently && !sap.IsDeleted)
                    .Select(sap => sap.AcademicProgramId)
                    .FirstOrDefault(),

                TotalEarnedHours = s.Enrollments
                    .Where(e => e.Status == EnrollmentStatus.Passed && !e.IsDeleted)
                    .Sum(e => (decimal?)e.CourseOffering.CreditHours) ?? 0,

                CurrentRegisteredHours = s.Enrollments
                    .Where(e => e.CourseOffering.SemesterId == semesterId
                             && e.Status == EnrollmentStatus.InProgress
                             && !e.IsDeleted)
                    .Sum(e => (decimal?)e.CourseOffering.CreditHours) ?? 0,

                ExistingEnrollments = s.Enrollments
                    .Where(e => e.CourseOffering.SemesterId == semesterId
                             && e.Status == EnrollmentStatus.InProgress
                             && !e.IsDeleted)
                    .SelectMany(e => e.TeachingSessionEnrollments.Where(ts => !ts.IsDeleted))
                    .Select(te => new StudentExistingEnrollment(
                        te.TeachingSessionId,
                        te.Enrollment.CourseOfferingId,
                        te.Enrollment.CourseOffering.Course.Name,
                        te.TeachingSession.Instructor.Name,
                        te.TeachingSession.Room.Building.Name,
                        te.TeachingSession.Room.RoomNumber,
                        te.Enrollment.GroupNumber,
                        te.TeachingSession.Type,
                        te.TeachingSession.StartTime,
                        te.TeachingSession.EndTime,
                        te.TeachingSession.Day
                    )).ToList()
            })
            .Select(temp => new
            {
                temp.Student,
                temp.CurrentProgramId,
                temp.CurrentRegisteredHours,
                temp.ExistingEnrollments,

                StudyLoad = _context.StudyLoadByLevels
                    .Where(sl => sl.Level.AcademicProgramId == temp.CurrentProgramId
                              && temp.TotalEarnedHours >= sl.Level.MinHours
                              && temp.TotalEarnedHours <= sl.Level.MaxHours
                              && sl.SemesterId == semesterId
                              && !sl.IsDeleted)
                    .Select(sl => new { sl.MinHours, sl.MaxHours })
                    .FirstOrDefault(),
                  StudentLevelName = _context.Levels
                    .Where(l=> !l.IsDeleted && temp.CurrentRegisteredHours >=l.MinHours && temp.CurrentRegisteredHours<=l.MaxHours) 
                    .Select(l => l.Name)
                    .FirstOrDefault()
            })
            .Select(temp => new EnrollmentValidationContextDto (
                temp.Student.Name,
                temp.Student.StudentCode,
                temp.StudentLevelName,
                temp.CurrentProgramId,
                _context.Semesters.Any(sem => sem.Id == semesterId && !sem.IsDeleted),
                temp.StudyLoad != null ? temp.StudyLoad.MinHours : null,
                temp.StudyLoad != null ? temp.StudyLoad.MaxHours : null,
                temp.CurrentRegisteredHours,
                temp.ExistingEnrollments
            ))
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);
    }
}