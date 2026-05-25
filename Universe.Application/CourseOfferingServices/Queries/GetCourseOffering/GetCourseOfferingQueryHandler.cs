using Universe.Core.Contracts.CourseOffering;

namespace Universe.Application.CourseOfferingServices.Queries.GetCourseOffering;

public class GetCourseOfferingQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetCourseOfferingQuery, Result<CourseOfferingWithDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<CourseOfferingWithDetailsResponse>> Handle(GetCourseOfferingQuery request, CancellationToken cancellationToken)
    {

        var response = await _cacheService.GetOrCreateAsync(
            key: CourseOfferingCacheKeys.ById(request.Id),
            factory: async () =>
            {
                var courseOffering = await _unitOfWork.CourseOfferingRepository
                    .GetByIdIncludingAssessmentsAsync(request.Id, cancellationToken);

                if (courseOffering is null)
                    return null;

                var semester = await _unitOfWork.AcademicYearRepository
                        .GetSemesterByIdAsync(courseOffering.SemesterId, cancellationToken);

                if (semester is null)
                    return null;

                return new CourseOfferingWithDetailsResponse
                (
                    courseOffering.Id,
                    courseOffering.NumberOfGroups,
                    courseOffering.CreditHours,
                    courseOffering.TotalGrade,
                    courseOffering.SuccessPercentage,
                    courseOffering.IsOptional,
                    courseOffering.OptionalGroupCode,
                    courseOffering.IsIncludedInGpa,
                    semester.Name,
                    courseOffering.CourseId,
                    semester.Id,
                    courseOffering.AcademicProgramId,
                    courseOffering.LevelId,
                    courseOffering.Assessments.Select(ass => new AssessmentDto
                    (
                        ass.Id,
                        ass.Type,
                        ass.MaxScore
                    )).ToList()
                );
            },
            cancellationToken: cancellationToken
        );

        if (response is null)
            return Result.Failure<CourseOfferingWithDetailsResponse>(CourseOfferingErrors.NotFound);

        return Result.Success(response);
    }
}