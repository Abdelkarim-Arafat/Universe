using Microsoft.EntityFrameworkCore; 
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

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

    public async Task UpdatePersonalDataAsync(Guid StudentId, CancellationToken cancellationToken)
        => await _context.Students
        .Where(x => x.Id == StudentId)
        .ExecuteUpdateAsync(setter =>
            setter.SetProperty(x => x.Name, x => x.Name)
        );

    public async Task<bool> IsStudentCodeExistsAsync(Guid CollegeId, Guid? UserId, string studentCode, CancellationToken cancellationToken)
        => await _context.Students
        .AnyAsync(x => x.CollegeId == CollegeId &&
        x.StudentCode == studentCode &&
        !x.IsDeleted &&
        (UserId == null || x.Id != UserId), cancellationToken);

    public async Task<bool> IsStudentNationalIdExistsAsync(Guid CollegeId, Guid? UserId, string NationalId, CancellationToken cancellationToken)
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

    public async Task<decimal> CalculateCreditHoursAsync(Guid StudentId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Where(x => x.StudentId == StudentId
            && x.Status == Core.Enums.EnrollmentStatus.Passed
            && !x.IsDeleted
            && !x.CourseOffering.IsDeleted)
           .SumAsync(x => x.CourseOffering.CreditHours, cancellationToken);
    }
    public async Task<decimal> CalculateCreditHoursAsync(Guid StudentId, Guid SemesterId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Where(x => x.StudentId == StudentId
            && x.Status == Core.Enums.EnrollmentStatus.Passed
            && x.CourseOffering.SemesterId == SemesterId
            && !x.IsDeleted
            && !x.CourseOffering.IsDeleted)
           .SumAsync(x => x.CourseOffering.CreditHours, cancellationToken);
    }

    public async Task<Level?> GetCurrentLevelAsync(Guid StudentId, CancellationToken cancellationToken)
    {
        int creditHours = (int)await CalculateCreditHoursAsync(StudentId, cancellationToken);
        return await _context.Levels
            .FirstOrDefaultAsync(lv => creditHours >= lv.MinHours
            && creditHours <= lv.MaxHours
            && !lv.IsDeleted, cancellationToken);
    }

    public async Task<decimal> CalculateComulativeGpaAsync(Guid StudentId, CancellationToken cancellationToken)
    {
        var totalQualityPoints = await _context.Enrollments
            .Where(e => e.StudentId == StudentId
            && e.Status == Core.Enums.EnrollmentStatus.Passed
            && e.CourseOffering.IsIncludedInGpa
            && !e.IsDeleted)
            .SumAsync(x => x.GradePoint * x.CourseOffering.CreditHours, cancellationToken);

        var totalEarnedCreditHours = await CalculateCreditHoursAsync(StudentId, cancellationToken);

        return totalEarnedCreditHours == 0 ? 0 : (decimal)totalQualityPoints / totalEarnedCreditHours;
    }
    public async Task<decimal> CalculateSemesterGpaAsync(Guid StudentId, Guid SemesterId, CancellationToken cancellationToken)
    {
        var totalQualityPoints = await _context.Enrollments
            .Where(e => e.StudentId == StudentId
            && e.Status == Core.Enums.EnrollmentStatus.Passed
            && e.CourseOffering.IsIncludedInGpa
            && e.CourseOffering.SemesterId == SemesterId
            && !e.IsDeleted)
            .SumAsync(x => x.GradePoint * x.CourseOffering.CreditHours, cancellationToken);

        var totalEarnedCreditHours = await CalculateCreditHoursAsync(StudentId, SemesterId, cancellationToken);

        return totalEarnedCreditHours == 0 ? 0 : (decimal)totalQualityPoints / totalEarnedCreditHours;
    }

    public async Task<List<StudentAssessment>> GetStudentAssessmentByCourseOfferingBulkAsync
        (List<Guid> ToRemoveCourses, Guid StudentId, CancellationToken cancellationToken)
    {
        return await _context.StudentAssessments.
             Where(ass => ass.StudentId == StudentId
             && ToRemoveCourses.Contains(ass.CourseOfferingId)
             && !ass.IsDeleted)
             .ToListAsync(cancellationToken);
    }
    // فصل كل شيء من بعضه
    public async Task<List<(Student Student, string StudentLevelName, List<StudentAssessment> Assessments)>>
        GetStudentsByCourseOfferingAndGroupNumberAsync(
     Guid courseOfferingId,
     int groupNumber,
     string? studentCodeOrName,
     CancellationToken cancellationToken)
    {
        var query = _context.Students
            .AsNoTracking()
            .Include(s => s.Enrollments)
            .Where(stu => stu.Enrollments.Any(e =>
                e.CourseOfferingId == courseOfferingId &&
                e.GroupNumber == groupNumber &&
                !e.IsDeleted));

        if (!string.IsNullOrWhiteSpace(studentCodeOrName))
        {
            query = query.Where(stu =>
                stu.StudentCode.Contains(studentCodeOrName) ||
                stu.Name.Contains(studentCodeOrName));
        }


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
}
