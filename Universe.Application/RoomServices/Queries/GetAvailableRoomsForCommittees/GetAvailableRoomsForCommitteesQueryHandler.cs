

using Universe.Core.Contracts.Rooms;

namespace Universe.Application.RoomServices.Queries.GetAvailableRoomsForCommittees;

public class GetAvailableRoomsForCommitteesQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService)
    : IRequestHandler<GetAvailableRoomsForCommitteesQuery, Result<PaginationList<AvailableRoomsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<AvailableRoomsResponse>>> Handle(GetAvailableRoomsForCommitteesQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.BuildingRepository.IsExistAsync(request.BuildingId, cancellationToken))
            return Result.Failure<PaginationList<AvailableRoomsResponse>>(BuildingErrors.NotFound);

        if (!await _unitOfWork.ExamRepository.IsExistExamTermAsync(request.examTermId, cancellationToken))
            return Result.Failure<PaginationList<AvailableRoomsResponse>>(ExamErrors.ExamTermNotFound);

        var filter = request.Filter;

        var cacheKey = RoomCacheKeys.AvailableForCommittees(request.BuildingId, request.examTermId, request.Filter);

        var tags = RoomCacheKeys.CommitteeTags(request.BuildingId, request.examTermId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<Room>().GetQueryable()
                    .AsNoTracking()
                    .Where(room => room.BuildingId == request.BuildingId
                                   && !room.IsDeleted
                                   && !room.ExamCommittees.Any(c => !c.IsDeleted && c.ExamTermId == request.examTermId));

          
                if (!string.IsNullOrEmpty(filter.SearchValue))
                    query = query.Where(x => x.Name.Contains(filter.SearchValue) || x.RoomNumber.ToString().Contains(filter.SearchValue));

                if (!string.IsNullOrEmpty(filter.SortColumn))
                    query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

                var source = query.Select(room => new AvailableRoomsResponse(
                    room.Id,
                    room.RoomNumber,
                    room.Capacity
                ));

                return await PaginationList<AvailableRoomsResponse>
                    .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
        );

        return Result.Success(response);
    }
}