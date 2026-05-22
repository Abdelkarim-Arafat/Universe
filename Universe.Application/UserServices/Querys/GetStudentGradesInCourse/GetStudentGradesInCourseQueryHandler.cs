using Universe.Core.Contracts.StudentAssessments;

namespace Universe.Application.UserServices.Querys.GetStudentGradesInCourse;

public class GetStudentGradesInCourseQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetStudentGradesInCourseQuery, Result<List<StudentAssessmentInCourseResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<StudentAssessmentInCourseResponse>>>
        Handle(GetStudentGradesInCourseQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.UserRepository.IsUserExistAsync(request.StudentId, cancellationToken))
            return Result.Failure<List<StudentAssessmentInCourseResponse>>(StudentErrors.UserNotFound);

        if (!await _unitOfWork.CourseOfferingRepository.IsExistAsync(request.CourseOfferingId, cancellationToken))
            return Result.Failure<List<StudentAssessmentInCourseResponse>>(CourseOfferingErrors.NotFound);

        var response = await _unitOfWork.StudentAssessmentRepository
            .GetStudentAssessmentsInCourseAsync(request.StudentId, request.CourseOfferingId, cancellationToken);

        return Result.Success(response);
    }
}
