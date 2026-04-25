using Universe.Application.CourseOfferingExamServices.Dtos;
namespace Universe.Application.CourseOfferingExamServices.Queries.Get;

public class GetCourseOfferingExamQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCourseOfferingExamQuery, Result<CourseOfferingExamResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingExamResponse>> Handle(GetCourseOfferingExamQuery request, CancellationToken cancellationToken)
    {
        var courseOfferingExam = await _unitOfWork.ExamRepository
            .GetCourseOfferingExamByIdAsync(request.Id, cancellationToken);
        if (courseOfferingExam == null)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.CourseOfferingExamNotFound);

        return Result.Success(courseOfferingExam.Adapt<CourseOfferingExamResponse>());
    }
}