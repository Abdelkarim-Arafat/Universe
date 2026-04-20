using Microsoft.EntityFrameworkCore; 
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Repositories;

public class UserRepository
    (ApplicationDbContext context,
    IAcademicProgramRepository academicProgramRepository,
    IGradeRepository gradeRepository) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAcademicProgramRepository _academicProgramRepository = academicProgramRepository;
    private readonly IGradeRepository _gradeRepository = gradeRepository;


    public IQueryable<ApplicationUser> GetAllStaffAsync()
        =>  _context.Users
            .Join(_context.UserRoles,
                user => user.Id,
                userRoles => userRoles.UserId,
                (user, userRole) => new { user, userRole.RoleId }
            )
        .Where(x => x.RoleId == DefaultRoles.Staff.Id && !x.user.IsDeleted)
        .Select(x => x.user)
        .Distinct();

    public async Task<bool> UserIsExistAsync(Guid Id, CancellationToken cancellationToken)
        => await _context.Users.AnyAsync(x => x.Id == Id && !x.IsDeleted, cancellationToken);

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken)
        => await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token && x.IsActive);

    //public async Task GetPasswordResetOtpAsync(Guid userId, string codeHash , CancellationToken cancellationToken)
    //    => await _context.PasswordResetOtps
    //        .SingleOrDefaultAsync(x => x.CodeHash == codeHash && x.ExpiresAt > DateTime.UtcNow);

    public async Task<Student?> GetStudentByIdAsync(Guid StudentId, CancellationToken cancellationToken)
        => await _context.Students.SingleOrDefaultAsync(x => x.Id == StudentId && !x.IsDeleted, cancellationToken);

    //public async Task UpdatePersonalDataAsync(Student student, CancellationToken cancellationToken)
    //{
    //    _context.Students.ExecuteUpdateAsync();
    //    await _context.SaveChangesAsync(cancellationToken);
    //}

    public async Task<List<Student>> GetStudentsByIdsAsync(
    List<Guid> studentIds,
    CancellationToken cancellationToken)
    {
        return await _context.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToListAsync(cancellationToken);
    }


    public async Task UpdatePersonalDataAsync(Guid StudentId, CancellationToken cancellationToken)
        => await _context.Students
        .Where(x => x.Id == StudentId)
        .ExecuteUpdateAsync(setter =>
            setter.SetProperty(x => x.Name, x => x.Name)
        );

    public async Task<bool> IsStudentCodeExistsAsync
        (Guid CollegeId, Guid? UserId, string studentCode, CancellationToken cancellationToken)
        => await _context.Students
        .AnyAsync(x => x.CollegeId == CollegeId &&
        x.StudentCode == studentCode &&
        !x.IsDeleted &&
        (UserId == null || x.Id != UserId), cancellationToken);

    public async Task<bool> IsStudentNationalIdExistsAsync
        (Guid CollegeId, Guid? UserId, string NationalId, CancellationToken cancellationToken)
        => await _context.Students
        .AnyAsync(x => x.CollegeId == CollegeId &&
        x.NationalIdOrPassport == NationalId &&
        !x.IsDeleted &&
        (UserId == null || x.Id == UserId), cancellationToken);

    public async Task<Guid?> GetCurrentProgram(Guid StudentId, CancellationToken cancellationToken)
     => await _context.StudentAcademicPrograms
         .AsNoTracking()
         .Where(x => x.StudentId == StudentId && x.Currently)
         .Select(x => x.AcademicProgramId)
         .FirstOrDefaultAsync(cancellationToken);

    public async Task<decimal> CalculateCreditHoursAsync
        (Guid StudentId,Guid? SemesterId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Where(x => x.StudentId == StudentId
            && x.Status == Core.Enums.EnrollmentStatus.Passed
            && ((SemesterId == null)|| (x.CourseOffering.SemesterId == SemesterId))
            && !x.IsDeleted
            && !x.CourseOffering.IsDeleted)
           .SumAsync(x => x.CourseOffering.CreditHours, cancellationToken);
    }
    public async Task<decimal> CalculateGpaAsync
        (Guid StudentId,Guid? SemesterId, CancellationToken cancellationToken)
    {
        var studentCourseGrades = await _context.Enrollments
          .AsNoTracking()
          .Where(e => e.StudentId == StudentId
                 && e.Status == Core.Enums.EnrollmentStatus.Passed
                 && e.CourseOffering.IsIncludedInGpa
                 && ((SemesterId == null)|| (e.CourseOffering.SemesterId == SemesterId))
                 && !e.IsDeleted)
          .Select(e => new
          {
            e.CourseOffering.CreditHours,
            TotalStudentDegree = e.Student.StudentAssessments
             .Where(sa => sa.CourseOfferingAssessment.CourseOfferingId == e.CourseOfferingId && !sa.IsDeleted)
             .Sum(sa => sa.degree ?? 0)
          })
        .ToListAsync(cancellationToken);

        var AcademicProgramId = await _academicProgramRepository
            .GetCurrentAcademicProgramIdAsync(StudentId, cancellationToken);

        var gradeScales = await _gradeRepository.GetProgramGradesAsync(AcademicProgramId.Value, cancellationToken);

        decimal totalQualityPoints = 0;

        foreach (var course in studentCourseGrades)
        {
            
            var points = gradeScales
                .FirstOrDefault(g => course.TotalStudentDegree >= g.MinScore
                                  && course.TotalStudentDegree <= g.MaxScore)
                ?.MinGradePoint ?? 0;

            totalQualityPoints += (points * course.CreditHours);
        }

        var totalEarnedCreditHours = await CalculateCreditHoursAsync(StudentId, SemesterId, cancellationToken);

        return totalEarnedCreditHours == 0 ? 0 : totalQualityPoints / totalEarnedCreditHours;
    }

    public async Task<List<StudentAssessment>> GetStudentAssessmentByCourseOfferingBulkAsync
        (List<Guid> ToRemoveCourses, Guid StudentId, CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments.
             Where(ass => ass.StudentId == StudentId
             && ToRemoveCourses.Contains(ass.CourseOfferingAssessment.CourseOfferingId)
             && !ass.IsDeleted)
             .ToListAsync(cancellationToken);
    }

    // فصل كل شيء من بعضه
    public async Task<List<(Student Student, string StudentLevelName, List<StudentAssessment> Assessments)>>
        GetStudentsByCourseOfferingAndGroupNumberAsync(
     Guid courseOfferingId,
     int? groupNumber,
     CancellationToken cancellationToken)
    {
        var query = _context.Students
            .AsNoTracking()
            .Include(s => s.Enrollments)
            .Where(stu => stu.Enrollments.Any(e =>
                e.CourseOfferingId == courseOfferingId &&
                (groupNumber == null || e.GroupNumber == groupNumber) &&
                !e.IsDeleted));

        var result = await query
            .Select(s => new
            {
                Student = s,
                TotalHours = s.Enrollments
                    .Where(e => e.Status == Core.Enums.EnrollmentStatus.Passed)
                    .Sum(e => e.CourseOffering.CreditHours),

                LevelName = _context.Levels
                    .Where(lv => !lv.IsDeleted &&
                                 s.Enrollments.Where(e => e.Status == Core.Enums.EnrollmentStatus.Passed)
                                              .Sum(e => e.CourseOffering.CreditHours) >= lv.MinHours &&
                                 s.Enrollments.Where(e => e.Status == Core.Enums.EnrollmentStatus.Passed)
                                              .Sum(e => e.CourseOffering.CreditHours) <= lv.MaxHours)
                    .Select(lv => lv.Name)
                    .FirstOrDefault(),
                Assessments = s.StudentAssessments.ToList()
            })
            .ToListAsync(cancellationToken);


        return result.Select(x => (x.Student, x.LevelName, x.Assessments)).ToList();
    }

    public async Task<List<StudentAssessment>> GetStudentsAssessmentsAsync(List<Guid> StudentsIds, CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments
            .Include(sa=>sa.CourseOfferingAssessment)
            .Where(sa => StudentsIds.Contains(sa.StudentId) && !sa.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<Guid, decimal>>
        GetAllStudentDegreesInCoursesAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments
            .AsNoTracking()
            .Where(sa => sa.StudentId == studentId && !sa.IsDeleted)
            .GroupBy(sa => sa.CourseOfferingAssessment.CourseOfferingId)
            .Select(g => new
            {
                CourseOfferingId = g.Key,
                TotalDegree = g.Sum(sa => sa.degree!.Value)
            })
            .ToDictionaryAsync(
                x => x.CourseOfferingId,
                x => x.TotalDegree,
                cancellationToken
            );
    }
    
}
