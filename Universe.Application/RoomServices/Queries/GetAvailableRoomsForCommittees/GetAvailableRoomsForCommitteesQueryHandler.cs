

using Universe.Core.Contracts.Rooms;

namespace Universe.Application.RoomServices.Queries.GetAvailableRoomsForCommittees;

public class GetAvailableRoomsForCommitteesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAvailableRoomsForCommitteesQuery, Result<PaginationList<AvailableRoomsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<AvailableRoomsResponse>>>
        Handle(GetAvailableRoomsForCommitteesQuery request, CancellationToken cancellationToken)
    {
        var isBuildingExist = await _unitOfWork.BuildingRepository
            .IsExistAsync(request.BuildingId, cancellationToken);

        if (!isBuildingExist)
            return Result.Failure<PaginationList<AvailableRoomsResponse>>(BuildingErrors.NotFound);

        var isExamTermExist = await _unitOfWork.ExamRepository.IsExistExamTermAsync(request.examTermId, cancellationToken);

        if (!isExamTermExist)
            return Result.Failure<PaginationList<AvailableRoomsResponse>>(ExamErrors.ExamTermNotFound);

        var query = _unitOfWork.Repository<Room>()
          .GetQueryable()
          .Where(room => room.BuildingId == request.BuildingId
           && !room.IsDeleted
           && !room.ExamCommittees.Any(committee => !committee.IsDeleted && committee.ExamTermId == request.examTermId));

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.Where(x => x.Name.Contains(filter.SearchValue));

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var source = query.Select(room => room.Adapt<AvailableRoomsResponse>());

        var response = await PaginationList<AvailableRoomsResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
