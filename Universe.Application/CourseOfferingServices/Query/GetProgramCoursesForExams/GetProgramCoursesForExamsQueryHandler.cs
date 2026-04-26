using Universe.Application.CourseOfferingServices.Dtos;

namespace Universe.Application.CourseOfferingServices.Query.GetProgramCoursesForExams;

public class GetProgramCoursesForExamsQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetProgramCoursesForExamsQuery, Result<PaginationList<CourseOfferingForExamsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<CourseOfferingForExamsResponse>>>
        Handle(GetProgramCoursesForExamsQuery request, CancellationToken cancellationToken)
    {
        var isProgramExist = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken);

        if (!isProgramExist)
            return Result.Failure<PaginationList<CourseOfferingForExamsResponse>>
                (AcademicProgramErrors.AcademicProgramNotFound);

        var query = _unitOfWork.Repository<CourseOffering>()
            .GetQueryable()
            .Where(course =>
               course.AcademicProgramId == request.AcademicProgramId
            && course.SemesterId ==  request.SemesterId
            &&!course.IsDeleted)
            .Select(courseOffering => new CourseOfferingForExamsResponse
            (courseOffering.Id,
             courseOffering.Course.Name,
             courseOffering.Course.Code,
             courseOffering.Enrollments.Count(enrol => !enrol.IsDeleted)
            ));

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.Where(x => x.CouresName
            .Contains(filter.SearchValue) || x.CouresCode.Contains(filter.SearchValue));

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var response = await PaginationList<CourseOfferingForExamsResponse>
            .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
