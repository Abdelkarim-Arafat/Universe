
using System.Linq;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetStudentAcademicHistory;

public class GetStudentAcademicHistoryCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetStudentAcademicHistoryCommand, Result<List<TranscriptSemesterResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<TranscriptSemesterResponse>>> Handle(GetStudentAcademicHistoryCommand request, CancellationToken cancellationToken)
    {
        var IsStudentExist = await _unitOfWork.UserRepository.UserIsExistAsync(request.StudentId, cancellationToken);
        if (!IsStudentExist)
            return Result.Failure<List<TranscriptSemesterResponse>>(StudentErrors.UserNotFound);
        var enrollments = await _unitOfWork.EnrollmentRepository.GetStudentEnrollmentsAsync(request.StudentId, cancellationToken);

        var summaries = await _unitOfWork.StudentSemesterSummaryRepository.
            GetStudentSummariesAsync(request.StudentId, cancellationToken);


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
