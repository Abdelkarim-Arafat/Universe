
using Universe.Core.Contracts.CourseOffering;

namespace Universe.Application.CourseOfferingServices.Queries.GetCourseOfferingAssessments;

public class GetCourseOfferingAssessmentsQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCourseOfferingAssessmentsQuery, Result<List<CourseOfferingAssessmentsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<CourseOfferingAssessmentsResponse>>>
        Handle(GetCourseOfferingAssessmentsQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.CourseOfferingRepository.IsExistAsync(request.CourseOfferingId, cancellationToken))
            return Result.Failure<List<CourseOfferingAssessmentsResponse>>(CourseOfferingErrors.NotFound);

        var assessments = await _unitOfWork.CourseOfferingRepository
            .GetCourseOfferingAssessmentsForViewAsync(request.CourseOfferingId, cancellationToken);

        return Result.Success(assessments);
    }
}