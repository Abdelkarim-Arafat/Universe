
using System.Security.Claims;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Enums;

namespace Universe.Application.UserServices.Querys.GetStudentAcademicHistory;

public class GetStudentAcademicHistoryCommandHandler(
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetStudentAcademicHistoryCommand, Result<List<TranscriptSemesterResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<List<TranscriptSemesterResponse>>> Handle(GetStudentAcademicHistoryCommand request, CancellationToken cancellationToken)
    {
        var User = _httpContextAccessor.HttpContext?.User;
        var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var StudentId = Guid.TryParse(value, out var userId) ? userId : Guid.Empty;
        var IsStudentExist = await _unitOfWork.UserRepository.UserIsExistAsync(StudentId, cancellationToken);

        if (!IsStudentExist)
            return Result.Failure<List<TranscriptSemesterResponse>>(StudentErrors.UserNotFound);
        var enrollments = await _unitOfWork.EnrollmentRepository.GetStudentEnrollmentsAsync(StudentId, cancellationToken);

        var groupedEnrollmentsBySemester = enrollments
            .GroupBy(e => e.CourseOffering.SemesterId)
            .OrderBy(g => g.First().CourseOffering.Semester.AcademicYear.StartDate)
            .ThenBy(g => g.First().CourseOffering.Semester.StartDate)
            .ToList();

        var AcademicProgramId = await _unitOfWork.AcademicProgramRepository
           .GetCurrentAcademicProgramIdAsync(StudentId, cancellationToken);

        if (!AcademicProgramId.HasValue)
            return Result.Failure<List<TranscriptSemesterResponse>>(StudentErrors.NoProgram);


        var allStudentDegreesInCourses = await _unitOfWork.UserRepository
            .GetAllStudentDegreesInCoursesAsync(StudentId, cancellationToken);

        var letterDegrees = await _unitOfWork.GradeRepository
            .GetProgramGradesAsync(AcademicProgramId.Value, cancellationToken);

        var response = new List<TranscriptSemesterResponse>();

        decimal totalQualityPoints = 0;
        decimal totalHours = 0;

        foreach (var enrollmentsInSemester in groupedEnrollmentsBySemester)
        {
            var firstEnrollment = enrollmentsInSemester.FirstOrDefault();
            if (firstEnrollment == null) continue;

            var semesterId = firstEnrollment.CourseOffering.SemesterId;

            // courses data 
            var courseGradeDtos = enrollmentsInSemester.Select(e =>
            {
                allStudentDegreesInCourses.TryGetValue(e.CourseOfferingId, out decimal finalGrade);

                var letter = letterDegrees
                    .FirstOrDefault(g => finalGrade >= g.MinScore && finalGrade <= g.MaxScore)?.Code ?? "-";

                return new CourseGradeDto(
                    e.CourseOffering.Course.Code,
                    e.CourseOffering.Course.Name,
                    e.CourseOffering.CreditHours,
                    letter,
                    finalGrade
                );
            }).ToList();

            // حساب gpas
            decimal semesterPoints = 0;
            decimal semesterHours = enrollmentsInSemester.Sum(e => e.CourseOffering.CreditHours);

            foreach (var course in courseGradeDtos)
            {
                var gradePoints = letterDegrees
                   .FirstOrDefault(g => course.FinalGrade >= g.MinScore && course.FinalGrade <= g.MaxScore)?
                   .MinGradePoint ?? 0; // لو شلتها هتضرب

                semesterPoints += (gradePoints * course.CreditHours);
            }

            decimal semesterGpa = semesterHours > 0 ? semesterPoints / semesterHours : 0;


            totalQualityPoints += semesterPoints;
            totalHours += semesterHours;
            decimal cumulativeGpa = totalHours > 0 ? totalQualityPoints / totalHours : 0;

            response.Add(new TranscriptSemesterResponse(
                firstEnrollment.CourseOffering.Semester.Name.ToString(),
                firstEnrollment.CourseOffering.Semester.AcademicYear.Name,
                semesterGpa,
                cumulativeGpa,
                semesterHours,
                enrollmentsInSemester.Where(e => e.Status == EnrollmentStatus.Passed).Sum(e => e.CourseOffering.CreditHours),
                letterDegrees.FirstOrDefault(ld => ld.MinGradePoint <= semesterGpa && ld.MaxGradePoint > semesterGpa)?.Code ?? "-",
                letterDegrees.FirstOrDefault(ld => ld.MinGradePoint <= cumulativeGpa && ld.MaxGradePoint > cumulativeGpa)?.Code ?? "-",
                courseGradeDtos
            ));
        }

        return Result.Success(response);
    }
}
