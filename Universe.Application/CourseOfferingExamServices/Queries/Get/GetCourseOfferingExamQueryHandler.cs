namespace Universe.Application.CourseOfferingExamServices.Queries.Get;

public class GetCourseOfferingExamQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCourseOfferingExamQuery, Result<CourseOfferingExamResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingExamResponse>> Handle(GetCourseOfferingExamQuery request, CancellationToken cancellationToken)
    {
        var courseOfferingExam = await _unitOfWork.ExamRepository
            .GetCourseOfferingExamByIdAsync(request.id, cancellationToken);

        if (courseOfferingExam == null)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.CourseOfferingExamNotFound);

        var response = new CourseOfferingExamResponse
            (
            courseOfferingExam.Id,
            courseOfferingExam.Date,
            courseOfferingExam.StartTime,
            courseOfferingExam.EndTime
            );

        return Result.Success(response);
    }
}