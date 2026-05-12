using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Contracts.Grades;
using Universe.Core.Contracts.Student;
using Universe.Core.Entities;
using Universe.Core.Enums;
using Universe.Core.Interfaces;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;
namespace Universe.Infrastructure.Repositories;

public class EnrollmentRepository(
    ApplicationDbContext context) : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Enrollment?> GetEnrollmentDataByCourseOfferingIdAsync
        (Guid courseOfferingId, Guid studentId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Where(e => e.CourseOfferingId == courseOfferingId
                     && e.StudentId == studentId
                     && !e.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);
    }

    // update
    public async Task<StudentAcademicHistoryContextDto> GetStudentAcademicHistoryContextAsync(
      Guid studentId,
      List<GradeResponse> letterDegrees,
      CancellationToken cancellationToken)
    {
        var studentEnrollments = await _context.Enrollments
            .AsNoTracking()
            .Where(e => e.StudentId == studentId && !e.IsDeleted)
            .Select(e => new
            {
                SemesterId = e.CourseOffering.Semester.Id,
                SemesterName = e.CourseOffering.Semester.Name.ToString(),
                AcademicYearName = e.CourseOffering.Semester.AcademicYear.Name,
                SemesterStartDate = e.CourseOffering.Semester.StartDate,
                AcademicYearStartDate = e.CourseOffering.Semester.AcademicYear.StartDate,

                e.CourseOfferingId,
                CourseCode = e.CourseOffering.Course.Code,
                CourseName = e.CourseOffering.Course.Name,
                CreditHours = e.CourseOffering.CreditHours,
                e.Status,

                TotalDegree = _context.StudentAssessments
                    .Where(sa => sa.StudentId == studentId
                              && sa.CourseOfferingAssessment.CourseOfferingId == e.CourseOfferingId
                              && !sa.IsDeleted)
                    .Sum(sa => sa.degree ?? 0)
            })
            .ToListAsync(cancellationToken);


        var semesterRecords = studentEnrollments
            .GroupBy(enrollment => new { enrollment.SemesterId, enrollment.SemesterName, enrollment.AcademicYearName, enrollment.SemesterStartDate, enrollment.AcademicYearStartDate })
            .OrderBy(group => group.Key.AcademicYearStartDate)
            .ThenBy(group => group.Key.SemesterStartDate)
            .Select(group => new StudentSemesterRecord(
                group.Key.SemesterId,
                group.Key.SemesterName,
                group.Key.AcademicYearName,
                group.Key.SemesterStartDate,
                group.Select(info => new CourseDetailsDto(
                    info.CourseCode,
                    info.CourseName,
                    info.CreditHours,
                    info.TotalDegree,
                     letterDegrees.FirstOrDefault(g =>
                       info.TotalDegree >= g.MinScore && info.TotalDegree <= g.MaxScore)?.Code ?? "-",
                    info.Status == EnrollmentStatus.Passed
                )).ToList()
            )).ToList();

        return new StudentAcademicHistoryContextDto(semesterRecords);
    }
    public async Task<List<StudentExistingEnrollment>> GetStudentScheduleAsync
        (Guid studentId, Guid currentSemesterId, CancellationToken cancellationToken)
    {
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
    // update
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
    // update 
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

    public async Task<List<Guid>> GetRegisteredCourseOfferingIdsInCurrentSemesterAsync
        (Guid studentId, Guid semesterId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Where(e => e.StudentId == studentId
                     && e.CourseOffering.SemesterId == semesterId
                     && e.Status == EnrollmentStatus.InProgress
                     && !e.IsDeleted)
            .Select(e => e.CourseOfferingId)
            .ToListAsync(cancellationToken);
    }
}