using Universe.Application.AcademicEventServices.EvenetDtos;
using Universe.Core.Contracts.User;

namespace Universe.Application.AcademicEventServices.Queries.Get_All_Events;

public class GetEventsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<GetEventsQuery, Result<PaginationList<EventResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<EventResponse>>> Handle(
        GetEventsQuery request,
        CancellationToken cancellationToken)
    {
        var filter = request.FilterRequest;

        var cacheKey = AcademicEventCacheKeys.List(
            request.ProgramId,
            request.SemesterId,
            filter.SortColumn,
            filter.SortDirection,
            filter.PageNumber,
            filter.PageSize
        );

        var tags = AcademicEventCacheKeys.Tags(request.ProgramId, request.SemesterId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var source = _unitOfWork.Repository<AcademicEvent>()
                    .GetQueryable()
                    .Where(x =>
                        x.ProgramId == request.ProgramId &&
                        x.SemesterId == request.SemesterId)
                    .OrderBy(x => x.StartDate)
                    .Select(x => new EventResponse(
                        x.Id.ToString(),
                        x.Type,
                        x.StartDate,
                        x.EndDate
                    ));

                return await PaginationList<EventResponse>
                    .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}