using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Queries.GetCourseSessions;

public class GetCourseSessionsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetCourseSessionsQuery, Result<IReadOnlyList<SessionResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<IReadOnlyList<SessionResponse>>> Handle(GetCourseSessionsQuery request, CancellationToken cancellationToken)
    {
        if (!(await _unitOfWork.CourseOfferingRepository
            .IsExistAsync(request.CourseOfferingId, cancellationToken))
            ) return Result.Failure<IReadOnlyList<SessionResponse>>(CourseOfferingErrors.NotFound);

        var response = await _cacheService.GetOrCreateAsync(
            key: SessionCacheKeys.CourseSessions(request.CourseOfferingId, request.GroupNumber),
            factory: async () => await _unitOfWork.CourseOfferingRepository
                            .GetCourseOfferingSessionsAsync(request.CourseOfferingId, request.GroupNumber, cancellationToken),
            cancellationToken: cancellationToken,
            tags: SessionCacheKeys.Tags(request.CourseOfferingId)
        );

        return Result.Success(response);
    }
}