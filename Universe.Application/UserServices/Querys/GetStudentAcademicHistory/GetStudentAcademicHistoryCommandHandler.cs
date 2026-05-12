 using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetStudentAcademicHistory;

public class GetStudentAcademicHistoryCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetStudentAcademicHistoryCommand, Result<List<StudentSemesterDataResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<List<StudentSemesterDataResponse>>> Handle(GetStudentAcademicHistoryCommand request, CancellationToken cancellationToken)
    {

        var studentCurrentProgramId = await _unitOfWork.AcademicProgramRepository
            .GetStudentCurrentProgramIdAsync(request.StudentId, cancellationToken);

        if (studentCurrentProgramId == null)
            return Result.Failure<List<StudentSemesterDataResponse>>(StudentErrors.NoProgram);

        var letterDegrees = await _unitOfWork.GradeRepository
            .GetProgramGradesAsync(studentCurrentProgramId.Value, cancellationToken);

        var studentHistory = await _unitOfWork.EnrollmentRepository
            .GetStudentAcademicHistoryContextAsync(request.StudentId, letterDegrees, cancellationToken);


        var response = new List<StudentSemesterDataResponse>();

        decimal totalQualityPoints = 0, totalHours = 0;

        foreach (var semester in studentHistory.Semesters)
        {

            var courseDetails = semester.Courses;

            // حساب gpas
            decimal semesterPoints = 0;
            decimal semesterHours = 0;
            decimal semesterPassedHourse = 0;

            foreach (var course in courseDetails)
            {
                var gradePoints = letterDegrees
                   .FirstOrDefault(g => course.TotalDegree >= g.MinScore && course.TotalDegree <= g.MaxScore)?
                   .MinGradePoint ?? 0; // لو شلتها هتضرب

                semesterPoints += (gradePoints * course.CreditHours);
                semesterHours += course.CreditHours;
                semesterPassedHourse += course.IsPassed ? course.CreditHours : 0;
            }

            decimal semesterGpa = semesterHours > 0 ? semesterPoints / semesterHours : 0;

            totalQualityPoints += semesterPoints;
            totalHours += semesterHours;
            decimal cumulativeGpa = totalHours > 0 ? totalQualityPoints / totalHours : 0;

            response.Add(new StudentSemesterDataResponse(
                semester.SemesterName,
                semester.AcademicYearName,
                semesterGpa,
                cumulativeGpa,
                semesterHours,
                semesterPassedHourse,
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
