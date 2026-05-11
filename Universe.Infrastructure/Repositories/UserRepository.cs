using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Universe.Core.Contracts.Control;
using Universe.Core.Contracts.Student;
using Universe.Core.Contracts.User;
using Universe.Core.Entities;
using Universe.Core.Enums;
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
        .Where(x => x.RoleId == RoleSeed.Staff.Id && !x.user.IsDeleted)
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
	public async Task<PersonalDataResponse?> GetStudentPersonalDataAsync(
	Guid studentId,
	CancellationToken cancellationToken = default)
	    => await _context.Students
			.AsNoTracking()
			.Where(x => x.Id == studentId)
			.ProjectToType<PersonalDataResponse>()
			.FirstOrDefaultAsync(cancellationToken);
	public async Task<ContactDataResponse?> GetStudentContactDataAsync(
	Guid studentId,
	CancellationToken cancellationToken = default)
	    => await _context.Students
			.AsNoTracking()
			.Where(x => x.Id == studentId)
			.Select(x => x.ContactInfo)
			.ProjectToType<ContactDataResponse>()
			.FirstOrDefaultAsync(cancellationToken);
	public async Task<ParentDataResponse?> GetStudentParentDataAsync(
	Guid studentId,
	CancellationToken cancellationToken = default)
		=> await _context.Students
			.AsNoTracking()
			.Where(x => x.Id == studentId)
			.Select(x => x.ParentInfo)
			.ProjectToType<ParentDataResponse>()
			.FirstOrDefaultAsync(cancellationToken);
	public async Task<MilitaryDataResponse?> GetStudentMilitaryDataAsync(
	Guid studentId,
	CancellationToken cancellationToken = default)
		=> await _context.Students
			.AsNoTracking()
			.Where(x => x.Id == studentId)
			.Select(x => x.MilitaryInfo!)
			.ProjectToType<MilitaryDataResponse>()
			.FirstOrDefaultAsync(cancellationToken);

	public async Task<PreviousQualificationResponse?> GetStudentPreviousQualificationAsync(
    Guid studentId,
    CancellationToken cancellationToken = default)
        => await _context.Students
            .AsNoTracking()
            .Where(x => x.Id == studentId)
            .Select(x => x.PreviousQualification)
            .ProjectToType<PreviousQualificationResponse>()
            .FirstOrDefaultAsync(cancellationToken);


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

	public async Task<bool> IsStudentCodeExistsAsync(
	Guid collegeId,
	Guid? userId,
	string studentCode,
	CancellationToken cancellationToken)
	=> await _context.StudentAcademicPrograms.AnyAsync(x =>
		x.AcademicProgram.CollegeId == collegeId &&
		x.Student.StudentCode == studentCode &&
		!x.Student.IsDeleted &&
		(userId == null || x.StudentId != userId),
		cancellationToken);

	public async Task<bool> IsStudentNationalIdExistsAsync(
	Guid collegeId,
	Guid? userId,
	string nationalId,
	CancellationToken cancellationToken)
	=> await _context.StudentAcademicPrograms.AnyAsync(x =>
		x.AcademicProgram.CollegeId == collegeId &&
		x.Student.NationalIdOrPassport == nationalId &&
		!x.Student.IsDeleted &&
		(userId == null || x.StudentId != userId),
		cancellationToken);

	public async Task<Guid?> GetCurrentProgram(Guid StudentId, CancellationToken cancellationToken)
     => await _context.StudentAcademicPrograms
         .AsNoTracking()
         .Where(x =>!x.IsDeleted && x.StudentId == StudentId && x.Currently)
         .Select(x => x.AcademicProgramId)
         .FirstOrDefaultAsync(cancellationToken);

    public async Task<decimal> CalculateCreditHoursAsync
        (Guid StudentId, Guid? SemesterId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Where(x => x.StudentId == StudentId
            && x.Status == Core.Enums.EnrollmentStatus.Passed
            && ((SemesterId == null) || (x.CourseOffering.SemesterId == SemesterId))
            && !x.IsDeleted
            && !x.CourseOffering.IsDeleted)
           .SumAsync(x => x.CourseOffering.CreditHours, cancellationToken);
    }
    public async Task<decimal> CalculateGpaAsync(Guid studentId, Guid? semesterId, CancellationToken cancellationToken)
    {
        var academicProgramId = await _academicProgramRepository
            .GetCurrentAcademicProgramIdAsync(studentId, cancellationToken);

        if (academicProgramId == null) return 0;

        var gradeScales = await _gradeRepository
            .GetProgramGradesAsync(academicProgramId.Value, cancellationToken);

        var coursePerformance = await _context.Enrollments
            .AsNoTracking()
            .Where(e => e.StudentId == studentId
                   && e.Status == EnrollmentStatus.Passed
                   && e.CourseOffering.IsIncludedInGpa
                   && (semesterId == null || e.CourseOffering.SemesterId == semesterId)
                   && !e.IsDeleted)
            .Select(e => new
            {
                e.CourseOffering.CreditHours,
         
                TotalScore = _context.StudentAssessments
                    .Where(sa => sa.StudentId == studentId
                              && sa.CourseOfferingAssessment.CourseOfferingId == e.CourseOfferingId
                              && !sa.IsDeleted)
                    .Sum(sa => sa.degree ?? 0)
            })
            .ToListAsync(cancellationToken);

        if (!coursePerformance.Any()) return 0;

        decimal totalQualityPoints = 0;
        decimal totalHours = 0;

        foreach (var item in coursePerformance)
        {
            var grade = gradeScales.FirstOrDefault(g => item.TotalScore >= g.MinScore && item.TotalScore <= g.MaxScore);
            var points = grade?.MinGradePoint ?? 0;

            totalQualityPoints += (points * item.CreditHours);
            totalHours += item.CreditHours;
        }

        return totalHours == 0 ? 0 : totalQualityPoints / totalHours;
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

    public async Task<ILookup<Guid, StudentDegreeValue>> GetStudentsAssessmentsLookupAsync(
     List<Guid> StudentsIds,
     Guid CourseOfferingId,
     CancellationToken cancellationToken)
    {
        var assessmentsList = await _context.StudentAssessments
            .Where(sa => StudentsIds.Contains(sa.StudentId)
                      && sa.CourseOfferingAssessment.CourseOfferingId == CourseOfferingId
                      && !sa.IsDeleted)
            .Select(sa => new
            {
                StudentId = sa.StudentId,
                assessments = new StudentDegreeValue(
                  sa.CourseOfferingAssessmentId,
                  sa.degree
            )
            })
            .ToListAsync(cancellationToken);

        return assessmentsList.ToLookup(sa => sa.StudentId, sa => sa.assessments);
    }
    public async Task<Dictionary<Guid, string>> GetStudentsLevelNameDictionaryAsync(
        List<Guid> StudentsIds,
        Guid programId,
        CancellationToken cancellationToken)
    {
        var levels = await _context.Levels
            .AsNoTracking()
            .Where(lv => !lv.IsDeleted && lv.AcademicProgramId == programId)
            .Select(lv => new
            {
                lv.Name,
                lv.MinHours,
                lv.MaxHours
            })
            .ToListAsync(cancellationToken);

        var studentsHours = await _context.Students
            .AsNoTracking()
            .Where(s => StudentsIds.Contains(s.Id) && !s.IsDeleted)
            .Select(s => new
            {
                StudentId = s.Id,
                TotalEarnedHours = s.Enrollments
                    .Where(e => e.Status == EnrollmentStatus.Passed && !e.IsDeleted) 
                    .Sum(e => e.CourseOffering.CreditHours)
            })
            .ToListAsync(cancellationToken);

        var studentsLevelDictionary = studentsHours.ToDictionary(
            s => s.StudentId,
            s => levels.FirstOrDefault
            (lv => s.TotalEarnedHours >= lv.MinHours && s.TotalEarnedHours <= lv.MaxHours)?.Name ?? "No Level"
        );

        return studentsLevelDictionary;
    }

    public async Task<StudentExamsResponse> GetStudentExamsTablesAsync(Guid studentId, CancellationToken cancellationToken)
    {
       // الحركة دي بتخلي الكويري يتكتب لكن ميتنفذش ف اللحظه دي و يتنفذ اول م اناديه و كده اوفر one hit للداتا بيز

        var currentProgramIdQuery = _context.StudentAcademicPrograms
            .Where(sp => sp.StudentId == studentId && sp.Currently && !sp.IsDeleted)
            .Select(sp => sp.AcademicProgramId);

        var today = DateOnly.FromDateTime(DateTime.Now);

        var currentYearId = await _context.AcademicYears
            .Where(ay => !ay.IsDeleted &&
                         today >= ay.StartDate &&
                         today <= ay.EndDate)
            .Select(ay => ay.Id)
            .FirstOrDefaultAsync(cancellationToken);

        // شيك علي موضوع ال current في السيميستر
        var currentSemesterIdQuery = _context.Semesters
            .Where(s => s.AcademicYearId == currentYearId && s.IsCurrent && !s.IsDeleted)
            .Select(s => s.Id);

        var examTermsQuery = _context.ExamTerms
            .Where(exam => !exam.IsDeleted
                && currentProgramIdQuery.Contains(exam.AcademicProgramId)
                && exam.SemesterId == currentSemesterIdQuery.FirstOrDefault()
                && exam.IsPublished)
            .Select(exam => exam.Id);

        var studentEnrollmentsQuery = _context.Enrollments
            .Where(e => e.StudentId == studentId 
            && currentSemesterIdQuery.Contains(e.CourseOffering.SemesterId)
            && e.Status == EnrollmentStatus.InProgress
            &&!e.IsDeleted)
            .Select(e => e.CourseOfferingId);
   
        var studentExams = await _context.CourseOfferingExams
            .AsNoTracking()
            .Where(ce => !ce.IsDeleted
                         && examTermsQuery.Contains(ce.ExamTermId)
                         && studentEnrollmentsQuery.Contains(ce.CourseOfferingId))
            .GroupBy(ce => ce.ExamTerm.ExamType)
            .Select(group => new StudentExam
            (
                group.Key.ToString(),
                group.Select(ce => new StudentExamPerCourse (
                    ce.Date,
                    ce.CourseOffering.Course.Name,
                    ce.CourseOffering.Course.Code,
                    ce.StartTime,
                    ce.EndTime,
                    ce.CourseOfferingCommittees
                        .Where(c => !c.IsDeleted)
                        .Select(c => $"{c.ExamCommittee.Room.RoomNumber} - {c.ExamCommittee.Room.Building.Name}")
                        .FirstOrDefault() ?? "No Place Now",

                    _context.ExamSeats
                        .Where(s => !s.IsDeleted && s.StudentId == studentId
                         && s.CourseOfferingCommittee.CourseOfferingExamId == ce.Id)
                        .Select(s => s.SeatNumber)
                        .FirstOrDefault(),

                    ce.CourseOfferingCommittees
                        .Where(c => !c.IsDeleted)
                        .Select(c => c.ExamCommittee.CommitteeNumber)
                        .FirstOrDefault()
                )
                {

                }).ToList()
            ))
            .ToListAsync(cancellationToken);

        var studentInfo = await _context.Students
            .Where(s => s.Id == studentId && !s.IsDeleted)
            .Select(s => new { s.Name, s.StudentCode })
            .FirstOrDefaultAsync(cancellationToken);

        return new StudentExamsResponse (
           studentInfo!.Name,
           studentInfo.StudentCode,
           studentExams
        );
    }
}
