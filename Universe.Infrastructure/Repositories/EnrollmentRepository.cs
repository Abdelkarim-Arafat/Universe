using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Universe.Core.Contracts.Enrollments;
using Universe.Core.Contracts.Grades;
using Universe.Core.Contracts.Student;
using Universe.Core.Entities;
using Universe.Core.Enums;
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

    // check later

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

    public async Task<List<Enrollment>> GetExistingEnrollmentIncludingSessionsAsync(
    Guid studentId,
    Guid semesterId,
    CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Include(e => e.TeachingSessionEnrollments.Where(ts => !ts.IsDeleted))
            .Where(e => !e.IsDeleted
                 && e.StudentId == studentId
                 && e.Status == EnrollmentStatus.InProgress
                 && e.CourseOffering.SemesterId == semesterId)
            .ToListAsync(cancellationToken);

    }
    public async Task<List<StudentExistingEnrollment>> GetExistingEnrollmentsInfoAsync
    (Guid studentId, Guid semesterId, CancellationToken cancellationToken)
    {
        return await _context.TeachingSessionEnrollments
            .AsNoTracking()
            .Where(te => te.Enrollment.StudentId == studentId
                      && te.Enrollment.CourseOffering.SemesterId == semesterId
                      && te.Enrollment.Status == EnrollmentStatus.InProgress
                      && !te.Enrollment.IsDeleted
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
            )).ToListAsync(cancellationToken);
    }
    public async Task<List<Guid>> GetRegisteredCourseOfferingIdsInCurrentSemesterAsync
        (Guid studentId, Guid semesterId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Where(e => e.StudentId == studentId
                     && e.CourseOffering.SemesterId == semesterId
                     && !e.IsDeleted)
            .Select(e => e.CourseOfferingId)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> CalculateCurrentRegisteredHoursAsync
        (Guid studentId, Guid semesterId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Where(e => e.StudentId == studentId
                     && e.Status == EnrollmentStatus.InProgress
                     && e.CourseOffering.SemesterId == semesterId
                     && !e.IsDeleted)
            .SumAsync(e => e.CourseOffering.CreditHours);
    }
}