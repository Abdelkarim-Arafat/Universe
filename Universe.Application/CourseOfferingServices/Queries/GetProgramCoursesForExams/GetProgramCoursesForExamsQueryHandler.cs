using Universe.Core.Contracts.CourseOfferings;

namespace Universe.Application.CourseOfferingServices.Queries.GetProgramCoursesForExams;

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
                (AcademicProgramErrors.NotFound);

        var isSemesterExist = await _unitOfWork.AcademicYearRepository
            .IsSemesterExistAsync(request.SemesterId, cancellationToken);

        if (!isSemesterExist)
            return Result.Failure<PaginationList<CourseOfferingForExamsResponse>>
                (SemesterErrors.NotFound);

        var isExamTermExist = await _unitOfWork.ExamRepository
             .IsExistExamTermAsync(request.examTermId, cancellationToken);

        if (!isExamTermExist)
            return Result.Failure<PaginationList<CourseOfferingForExamsResponse>>
                (ExamErrors.ExamTermNotFound);

        var query = _unitOfWork.Repository<CourseOffering>()
        .GetQueryable()
        .Where(course =>
                 !course.IsDeleted
               && course.AcademicProgramId == request.AcademicProgramId
               && course.SemesterId == request.SemesterId)
        .Select(courseOffering => new CourseOfferingForExamsResponse
               (
                 courseOffering.Id,
                 courseOffering.CourseOfferingExams
                .Where(exam => !exam.IsDeleted && exam.ExamTermId == request.examTermId)
                .Select(exam => exam.Id).FirstOrDefault(),
                 courseOffering.Course.Name,
                 courseOffering.Course.Code,
                 courseOffering.Enrollments.Count(enrol => !enrol.IsDeleted)
               ));

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.ApplySearch(filter.SearchValue, x => x.CouresName, x => x.CouresCode);

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var response = await PaginationList<CourseOfferingForExamsResponse>
                      .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}