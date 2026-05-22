using Universe.Core.Contracts.CourseOffering;

namespace Universe.Application.CourseOfferingServices.Queries.GetProgramCoursesForExams;

public class GetProgramCoursesForExamsQueryHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<GetProgramCoursesForExamsQuery, Result<PaginationList<CourseOfferingForExamsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<CourseOfferingForExamsResponse>>> Handle(
        GetProgramCoursesForExamsQuery request,
        CancellationToken cancellationToken)
    {
        var isProgramExist = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken);

        if (!isProgramExist)
            return Result.Failure<PaginationList<CourseOfferingForExamsResponse>>(AcademicProgramErrors.NotFound);

        var isSemesterExist = await _unitOfWork.AcademicYearRepository
            .IsSemesterExistAsync(request.SemesterId, cancellationToken);

        if (!isSemesterExist)
            return Result.Failure<PaginationList<CourseOfferingForExamsResponse>>(SemesterErrors.NotFound);

        var filter = request.Filter;

        var query = _unitOfWork.Repository<CourseOffering>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x =>
                !x.IsDeleted &&
                x.AcademicProgramId == request.AcademicProgramId &&
                x.SemesterId == request.SemesterId);

        if (!string.IsNullOrWhiteSpace(filter.SearchValue))
        {
            query = query.Where(x =>
                x.Course.Name.Contains(filter.SearchValue) ||
                x.Course.Code.Contains(filter.SearchValue));
        }

        var source = query.Select(x => new CourseOfferingForExamsResponse(
            x.Id,
            x.Course.Name,
            x.Course.Code,
            x.Enrollments.Count(e => !e.IsDeleted && !e.Student.IsDeleted),
            x.CourseOfferingExams
                    .Where(coe => !coe.IsDeleted && coe.ExamTermId == request.examTermId)
                    .Select(coe => coe.Id)
                    .FirstOrDefault(),
            x.CourseOfferingExams
                    .Any(coe => !coe.IsDeleted && coe.ExamTermId == request.examTermId)
        ));

        var response = await PaginationList<CourseOfferingForExamsResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}