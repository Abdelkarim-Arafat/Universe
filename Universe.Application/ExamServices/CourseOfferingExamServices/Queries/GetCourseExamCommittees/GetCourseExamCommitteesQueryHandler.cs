using Universe.Core.Contracts.ExamCommittees;

namespace Universe.Application.ExamServices.CourseOfferingExamServices.Queries.GetCourseExamCommittees;

public class GetCourseExamCommitteesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetCourseExamCommitteesQuery, Result<PaginationList<CourseExamCommitteesResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<CourseExamCommitteesResponse>>>
     Handle(GetCourseExamCommitteesQuery request, CancellationToken cancellationToken)
    {
        var isCourseOfferingExamExist = await _unitOfWork.Repository<CourseOfferingExam>()
            .GetQueryable()
            .AnyAsync(e => e.Id == request.id && !e.IsDeleted, cancellationToken);

        if (!isCourseOfferingExamExist)
            return Result.Failure<PaginationList<CourseExamCommitteesResponse>>(ExamErrors.CourseOfferingExamNotFound);

        var query = _unitOfWork.Repository<CourseOfferingCommittee>()
            .GetQueryable()
            .AsNoTracking()
            .Where(com => !com.IsDeleted && com.CourseOfferingExamId == request.id);

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.Where(com =>com.ExamCommittee.CommitteeNumber.ToString().Contains(filter.SearchValue));

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        

        var totalCount = await query.CountAsync(cancellationToken);


        var pagedData = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(com => new CourseExamCommitteesResponse
            (
                com.ExamCommitteeId,
                com.ExamCommittee.CommitteeNumber,
                com.ExamCommittee.MaxCapacity,
                com.ExamSeats.Count(seat => !seat.IsDeleted),
                com.ExamSeats.Where(seat => !seat.IsDeleted).Select(seat => seat.SeatNumber).Min(),
                com.ExamCommittee.Room != null
                    ? $"{com.ExamCommittee.Room.RoomNumber} - {com.ExamCommittee.Room.Building.Name}"
                    : "No Place"
            ))
            .ToListAsync(cancellationToken);

        var courseCommittees = new PaginationList<CourseExamCommitteesResponse>(
            pagedData,
            totalCount,
            filter.PageNumber,
            filter.PageSize
        );

        return Result.Success(courseCommittees);
    }
}
