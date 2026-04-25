using Universe.Application.RoomServices.Dtos;

namespace Universe.Application.RoomServices.Queries.GetAvailableRoomsForExam;

public class GetAvailableRoomsForExamQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAvailableRoomsForExamQuery, Result<PaginationList<AvailableRoomsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<AvailableRoomsResponse>>>
        Handle(GetAvailableRoomsForExamQuery request, CancellationToken cancellationToken)
    {
        var isBuildingExist = await _unitOfWork.BuildingRepository
            .IsExistAsync(request.BuildingId, cancellationToken);

        if (!isBuildingExist)
            return Result.Failure<PaginationList<AvailableRoomsResponse>>(BuildingErrors.NotFound);

        var courseOfferingExam = await _unitOfWork.ExamRepository
            .GetCourseOfferingExamByIdAsync(request.CourseOfferingExamId, cancellationToken);

        if (courseOfferingExam == null)
            return Result.Failure<PaginationList<AvailableRoomsResponse>>(ExamErrors.CourseOfferingExamNotFound);

        var date = courseOfferingExam.Date;
        var endTime = courseOfferingExam.EndTime;
        var startTime = courseOfferingExam.StartTime;

        var query = _unitOfWork.Repository<Room>()
          .GetQueryable()
          .Where(room => room.BuildingId == request.BuildingId
           && !room.IsDeleted
           && !room.ExamCommittees
           .Any(committee => committee.CourseOfferingExam.Date == date
           && ((committee.CourseOfferingExam.StartTime < endTime && startTime < committee.CourseOfferingExam.EndTime))));

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
