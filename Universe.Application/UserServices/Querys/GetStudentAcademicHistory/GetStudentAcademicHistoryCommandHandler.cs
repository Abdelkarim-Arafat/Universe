using System.Security.Claims;
using Universe.Application.UserServices.UserDtos;

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

        var studentHistoryDate = await _unitOfWork.EnrollmentRepository
            .GetStudentAcademicHistoryContextAsync(StudentId, cancellationToken);

        if (studentHistoryDate == null)
            return Result.Failure<List<TranscriptSemesterResponse>>(StudentErrors.UserNotFound);

        if (studentHistoryDate.CurrentAcademicProgramId == null)
            return Result.Failure<List<TranscriptSemesterResponse>>(StudentErrors.NoProgram);

        var letterDegrees = await _unitOfWork.GradeRepository
            .GetProgramGradesAsync(studentHistoryDate.CurrentAcademicProgramId.Value, cancellationToken); 

        var response = new List<TranscriptSemesterResponse>();

        decimal totalQualityPoints = 0, totalHours = 0;

        foreach (var semester in studentHistoryDate.Semesters)
        {

            // courses data 
            var courseDetails = semester.Courses.Select(course =>
            new CourseDetailsDto(
                    course.CourseCode,
                    course.CourseName,
                    course.CreditHours,
                    letterDegrees.FirstOrDefault(g => 
                       course.TotalGrade >= g.MinScore && course.TotalGrade <= g.MaxScore)?.Code ?? "-",

                    course.TotalGrade
                    )).ToList();

            // حساب gpas
            decimal semesterPoints = 0;
            decimal semesterHours = semester.Courses.Sum(course => course.CreditHours);

            foreach (var course in courseDetails)
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
                semester.SemesterName,
                semester.AcademicYearName,
                semesterGpa,
                cumulativeGpa,
                semesterHours,
                semester.Courses.Where(e => e.IsPassed).Sum(e => e.CreditHours),
                letterDegrees.FirstOrDefault(ld => 
                ld.MinGradePoint <= semesterGpa && ld.MaxGradePoint > semesterGpa)?.Code ?? "-",

                letterDegrees.FirstOrDefault(ld => 
                ld.MinGradePoint <= cumulativeGpa && ld.MaxGradePoint > cumulativeGpa)?.Code ?? "-",
                courseDetails
            ));
        }

        return Result.Success(response);
    }
}
