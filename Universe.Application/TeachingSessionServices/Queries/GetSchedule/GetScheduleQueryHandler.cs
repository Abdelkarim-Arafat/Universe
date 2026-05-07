
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Queries.GetSchedule;

public class GetScheduleQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetScheduleQuery , Result<ScheduleResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<ScheduleResponse>> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
    {
        var response = await _cacheService.GetOrCreateAsync(
            key: SessionCacheKeys.Schedule(request.ProgramId, request.SemesterId),
            factory: async () =>
            {
                var schedule = await _unitOfWork.AcademicProgramRepository
                    .GetScheduleAsync(request.ProgramId, request.SemesterId, cancellationToken);

                if (schedule is null)
                    return null;

                return schedule.Adapt<ScheduleResponse>();
            },
            cancellationToken: cancellationToken
        );

        if (response is null) 
            return Result.Failure<ScheduleResponse>(AcademicProgramErrors.ScheduleNotFound);

        return Result.Success(response);
    }
}
