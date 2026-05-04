using Universe.Application.ExamServices.ExamTermServices.Dtos;

namespace Universe.Application.ExamServices.ExamTermServices.Queries.GetProgramExams;

public class GetProgramExamsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetProgramExamsQuery, Result<PaginationList<ExamTermResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<ExamTermResponse>>> Handle(GetProgramExamsQuery request, CancellationToken cancellationToken)
    {
        var IsSemesterExist = await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(request.SemesterId, cancellationToken);

        if (!IsSemesterExist)
            return Result.Failure<PaginationList<ExamTermResponse>>(SemesterErrors.NotFound);

        var IsProgramExists = await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken);

        if (!IsProgramExists)
            return Result.Failure<PaginationList<ExamTermResponse>>(AcademicProgramErrors.NotFound);

        var query = _unitOfWork.Repository<ExamTerm>().GetQueryable()
            .Where(exam => exam.AcademicProgramId == request.AcademicProgramId
            && exam.SemesterId == request.SemesterId
            && !exam.IsDeleted);

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        
        var source = query.Select(x => x.Adapt<ExamTermResponse>());

        var response = await PaginationList<ExamTermResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}