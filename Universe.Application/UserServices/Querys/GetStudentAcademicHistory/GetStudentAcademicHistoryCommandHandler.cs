
using System.Linq;
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
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var StudentId = Guid.TryParse(value, out var userId) ? userId : Guid.Empty;
        var IsStudentExist = await _unitOfWork.UserRepository.UserIsExistAsync(StudentId, cancellationToken);
        if (!IsStudentExist)
            return Result.Failure<List<TranscriptSemesterResponse>>(StudentErrors.UserNotFound);
        var enrollments = await _unitOfWork.EnrollmentRepository.GetStudentEnrollmentsAsync(StudentId, cancellationToken);

        var summaries = await _unitOfWork.StudentSemesterSummaryRepository.
            GetStudentSummariesAsync(StudentId, cancellationToken);


        var response = summaries.Select(summary =>

            new TranscriptSemesterResponse
            (
                summary.Semester.Name.ToString(),
                summary.Semester.AcademicYear.Name,
                summary.SemesterGPA,
                summary.CumulativeGPA,
                summary.AttemptedHours,
                summary.TotalHoursEarned,
                summary.SemesterGrade,
                summary.CumulativeGrade,

                enrollments
                .Where(e => e.CourseOffering.SemesterId == summary.SemesterId)
                .Select(e => new CourseGradeDto
                (
                 e.CourseOffering.Course.Code,
                 e.CourseOffering.Course.Name,
                 e.CourseOffering.CreditHours,
                 e.LetterGrade ?? "-",
                 e.FinalGrade
                )).ToList()
           )).ToList();

        return Result.Success(response);
    }
}
