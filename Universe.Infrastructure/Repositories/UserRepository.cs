using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
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
    IGradeRepository gradeRepository) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;
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

    public async Task<bool> IsUserExistAsync(Guid Id, CancellationToken cancellationToken)
        => await _context.Users.AnyAsync(x => x.Id == Id && !x.IsDeleted, cancellationToken);

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken)
        => await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token && x.IsActive, cancellationToken);

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
        .Select(x => new PersonalDataResponse(
            x.Name,
            x.StudentCode,
            x.NationalIdOrPassport,
            x.Religion,
            x.Gender,
            x.DateOfBirth,
            x.MaritalStatus,
            x.PlaceOfBirth,
            x.Nationality
        ))
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<ContactDataResponse?> GetStudentContactDataAsync(
        Guid studentId,
        CancellationToken cancellationToken = default)
        => await _context.Students
            .AsNoTracking()
            .Where(x => x.Id == studentId)
            .Select(x => new ContactDataResponse(
                x.ContactInfo.City,
                x.ContactInfo.Address,
                x.ContactInfo.PostalCode,
                x.ContactInfo.PhoneNumber,
                x.ContactInfo.Email
            ))
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<ParentDataResponse?> GetStudentParentDataAsync(
        Guid studentId,
        CancellationToken cancellationToken = default)
        => await _context.Students
            .AsNoTracking()
            .Where(x => x.Id == studentId)
            .Select(x => new ParentDataResponse(
                x.ParentInfo.GuardianName,
                x.ParentInfo.RelationshipDegree,
                x.ParentInfo.Job,
                x.ParentInfo.MotherName,
                x.ParentInfo.GuardianCity,
                x.ParentInfo.GuardianEmail,
                x.ParentInfo.GuardianPhoneNumber,
                x.ParentInfo.GuardianAddress
            ))
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<MilitaryDataResponse?> GetStudentMilitaryDataAsync(
        Guid studentId,
        CancellationToken cancellationToken = default)
        => await _context.Students
            .AsNoTracking()
            .Where(x => x.Id == studentId)
            .Select(x => new MilitaryDataResponse(
                x.MilitaryInfo!.MilitaryStatus,
                x.MilitaryInfo.MilitaryNumber,
                x.MilitaryInfo.DecisionNumber,
                x.MilitaryInfo.DecisionDate,
                x.MilitaryInfo.EnrollmentDate,
                x.MilitaryInfo.EndDate
            ))
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<PreviousQualificationResponse?> GetStudentPreviousQualificationAsync(
        Guid studentId,
        CancellationToken cancellationToken = default)
        => await _context.Students
            .Where(x => x.Id == studentId)
            .Select(x => new PreviousQualificationResponse(
                x.PreviousQualification.SchoolName,
                x.PreviousQualification.EnrollmentYear,
                x.PreviousQualification.SeatNumber,
                x.PreviousQualification.Qualification,
                x.PreviousQualification.GraduationYear,
                x.PreviousQualification.TotalGrade,
                x.PreviousQualification.AdmissionType
            ))
            .FirstOrDefaultAsync(cancellationToken);

    //public async Task UpdatePersonalDataAsync(Student student, CancellationToken cancellationToken)
    //{
    //    _context.Students.ExecuteUpdateAsync();
    //    await _context.SaveChangesAsync(cancellationToken);
    //}

    public async Task<GraduationDetailsResponse?> GetStudentGraduationDetailsAsync(Guid studentId, Guid programId, CancellationToken cancellationToken)
    {
        var gpa = await CalculateGpaAsync(studentId, null, programId, cancellationToken);

        return await _context.Students
            .AsNoTracking()
            .Where(x => x.Id == studentId)
            .Select(x => new GraduationDetailsResponse(
                gpa,
                x.GraduationYear,
                x.GraduationSemester,
                x.GraduationProjectName
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }

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
        , cancellationToken);

    public async Task<bool> IsStudentCodeExistsAsync(
    Guid collegeId,
    Guid? userId,
    string studentCode,
    CancellationToken cancellationToken)
    => await _context.StudentAcademicPrograms.AnyAsync(x =>
               x.AcademicProgram.CollegeId == collegeId
            && x.Student.StudentCode == studentCode
            && !x.Student.IsDeleted &&
            (userId == null || x.StudentId != userId), cancellationToken);

	public async Task<bool> IsStudentNationalIdExistsAsync (
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

    public async Task<decimal> CalculateCreditHoursAsync
        (Guid StudentId, Guid? SemesterId, CancellationToken cancellationToken)
    {
        return await _context.Enrollments
            .Where(x => x.StudentId == StudentId
            && x.Status == EnrollmentStatus.Passed
            && ((SemesterId == null) || (x.CourseOffering.SemesterId == SemesterId))
            && !x.IsDeleted
            && !x.CourseOffering.IsDeleted)
           .SumAsync(x => x.CourseOffering.CreditHours, cancellationToken);
    }

    // update
    public async Task<decimal> CalculateGpaAsync(Guid studentId, Guid? semesterId, Guid programId, CancellationToken cancellationToken)
    {

        var gradeScales = await _gradeRepository
            .GetProgramGradesAsync(programId, cancellationToken);

        var courseData = await _context.Enrollments
            .Where(e => e.StudentId == studentId
                   && e.Status != EnrollmentStatus.InProgress
                   && e.CourseOffering.IsIncludedInGpa
                   && (semesterId == null || e.CourseOffering.SemesterId == semesterId)
                   && !e.IsDeleted
            )
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

        if (courseData.Count == 0)
            return 0;

        decimal totalQualityPoints = 0;
        decimal totalHours = 0;

        foreach (var item in courseData)
        {
            var grade = gradeScales.FirstOrDefault(g => item.TotalScore >= g.MinScore && item.TotalScore <= g.MaxScore);
            var points = grade?.MinGradePoint ?? 0;

            totalQualityPoints += (points * item.CreditHours);
            totalHours += item.CreditHours;
        }

        return totalHours == 0 ? 0 : totalQualityPoints / totalHours;
    }

    // update
    public async Task<Dictionary<Guid, string>> GetStudentsLevelNameDictionaryAsync(
        List<Guid> StudentsIds,
        Guid programId,
        CancellationToken cancellationToken)
    {
        var levels = await _context.Levels
            .Where(lv => !lv.IsDeleted && lv.AcademicProgramId == programId)
            .Select(lv => new
            {
                lv.Name,
                lv.MinHours,
                lv.MaxHours
            })
            .ToListAsync(cancellationToken);

        var studentsHours = await _context.Students
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

    public async Task<List<StudentExam>> GetStudentExamsTablesAsync(
    Guid studentId,
    List<Guid> currentCoursesIds,
    List<Guid> examTermsIds,
    CancellationToken cancellationToken)
    {
        var examsData = await _context.CourseOfferingExams
            .Where(ce => !ce.IsDeleted
                         && examTermsIds.Contains(ce.ExamTermId)
                         && currentCoursesIds.Contains(ce.CourseOfferingId))
            .Select(coe => new
            {
                coe.ExamTerm.ExamType,
                coe.Date,
                coe.StartTime,
                coe.EndTime,
                CourseName = coe.CourseOffering.Course.Name,
                CourseCode = coe.CourseOffering.Course.Code,

                Seat = _context.ExamSeats
                    .Where(seat => !seat.IsDeleted
                              && seat.StudentId == studentId
                              && seat.CourseOfferingCommittee.CourseOfferingExamId == coe.Id)
                    .Select(seat => new
                    {
                        seat.SeatNumber,
                        seat.CourseOfferingCommittee.ExamCommittee.CommitteeNumber,
                        seat.CourseOfferingCommittee.ExamCommittee.Room.RoomNumber,
                        BuildingName = seat.CourseOfferingCommittee.ExamCommittee.Room.Building.Name
                    })
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        var result = examsData
            .GroupBy(x => x.ExamType)
            .Select(group => new StudentExam
            (
                group.Key.ToString(),
                group.Select(info => new StudentExamPerCourse(
                    info.Date,
                    info.CourseName,
                    info.CourseCode,
                    info.StartTime,
                    info.EndTime,
                    info.Seat != null ? $"{info.Seat.RoomNumber} - {info.Seat.BuildingName}" : "No Place Assigned",
                    info.Seat?.SeatNumber ?? 0,  
                    info.Seat?.CommitteeNumber ?? 0
                )).ToList()
            ))
            .ToList();

        return result;
    }

    public async Task<Guid?> GetStudentCollegeIdAsync(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _context.StudentAcademicPrograms
             .Where(sap => !sap.IsDeleted && sap.StudentId == studentId && sap.Currently)
             .Select(sap => sap.AcademicProgram.CollegeId)
             .FirstOrDefaultAsync(cancellationToken);
    }
}
