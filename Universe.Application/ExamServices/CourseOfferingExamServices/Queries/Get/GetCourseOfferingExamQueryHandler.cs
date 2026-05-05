using Universe.Application.CourseOfferingExamServices.Dtos;
namespace Universe.Application.CourseOfferingExamServices.Queries.Get;

public class GetCourseOfferingExamQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCourseOfferingExamQuery, Result<CourseOfferingExamResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingExamResponse>> Handle(GetCourseOfferingExamQuery request, CancellationToken cancellationToken)
    {
        var courseOfferingExam = await _unitOfWork.ExamRepository
            .GetCourseOfferingExamByIdAsync(request.id, cancellationToken);

        if (courseOfferingExam == null)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.CourseOfferingExamNotFound);

        var query = _unitOfWork.Repository<CourseOfferingCommittee>()
                  .GetQueryable()
                  .AsNoTracking()
                  .Where(com => !com.IsDeleted && com.CourseOfferingExamId == courseOfferingExam.Id);

        var filter = request.Filter;

        var source = query.Select(com => new CourseExamCommittees
               (
                com.ExamCommitteeId,
                com.ExamCommittee.CommitteeNumber,
                com.ExamCommittee.MaxCapacity,
                com.ExamSeats.Count(seat => !seat.IsDeleted),
                com.ExamSeats.Where(seat => !seat.IsDeleted).Select(seat => (int?)seat.SeatNumber).Min() ?? 0, // check if null
                com.ExamCommittee.Room != null
                ? $"{com.ExamCommittee.Room.RoomNumber} - {com.ExamCommittee.Room.Building.Name}"
                : "No Place"
             ));

        if (!string.IsNullOrEmpty(filter.SearchValue))
            source = source.Where(x =>
                x.Place.Contains(filter.SearchValue) ||
                x.CommitteeNumber.ToString().Contains(filter.SearchValue));


        if (!string.IsNullOrEmpty(filter.SortColumn))
            source = source.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var courseCommittees = await PaginationList<CourseExamCommittees>
             .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        var response = new CourseOfferingExamResponse
            (
            courseOfferingExam.Id,
            courseOfferingExam.Date,
            courseOfferingExam.StartTime,
            courseOfferingExam.EndTime,
            courseCommittees
            );

        return Result.Success(response);
    }
}