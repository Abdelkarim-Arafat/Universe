using Universe.Application.ControlServices.Dtos;

namespace Universe.Application.ControlServices.Queries.GetLevelsCoursesStatistics;

public class GetCourseOfferingsControlStatisticsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetCourseOfferingsControlStatisticsQuery, Result<List<GetCourseOfferingsControlStatisticsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<List<GetCourseOfferingsControlStatisticsResponse>>> Handle(
        GetCourseOfferingsControlStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _cacheService.GetOrCreateAsync(
            key: ControlCacheKeys.CourseOfferingsStatistics(
                request.ProgramId,
                request.SemesterId
            ),
            factory: async () =>
            {
                return await _unitOfWork.Repository<Level>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(l => l.AcademicProgramId == request.ProgramId
                        && l.CourseOfferings.Any(co => co.SemesterId == request.SemesterId)
                        && !l.IsDeleted)
                    .Select(l => new GetCourseOfferingsControlStatisticsResponse(
                        l.Id,
                        l.Name,
                        l.CourseOfferings
                            .Where(co => co.SemesterId == request.SemesterId && !co.IsDeleted)
                            .Select(co => new CourseOfferingStatisticsResponse(
                                co.Id,
                                co.Course.Name,
                                co.Course.Code,

                                co.Enrollments
                                    .Count(e => !e.IsDeleted),

                                co.Enrollments
                                    .Where(e => !e.IsDeleted)
                                    .Count(e => e.Student.StudentAssessments
                                        .Any(sa =>
                                            sa.CourseOfferingAssessment.CourseOfferingId == co.Id &&
                                            !sa.IsDeleted &&
                                            !sa.degree.HasValue)),

                                co.IsOpenForControl
                            ))
                            .ToList()
                    ))
                    .ToListAsync(cancellationToken);
            },
            cancellationToken: cancellationToken
        );

        return Result.Success(response);
    }
}