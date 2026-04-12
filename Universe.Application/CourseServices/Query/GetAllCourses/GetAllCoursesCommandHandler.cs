using Universe.Application.CourseServices.Dtos;
 

namespace Universe.Application.CourseServices.Query.GetAllCourses;

public class GetAllCoursesCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCoursesCommand, Result<PaginationList<CourseResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<CourseResponse>>> Handle(GetAllCoursesCommand request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Course>()
            .GetQueryable()
            .Where(x => x.CollegeId == request.CollegeId);

        var filter = request.filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }


        var source = query.Select(c=>c.Adapt<CourseResponse>());

        var response = await PaginationList<CourseResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
