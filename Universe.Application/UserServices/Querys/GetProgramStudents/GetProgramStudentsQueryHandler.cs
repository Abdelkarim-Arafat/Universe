
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetAllStudents;

public class GetProgramStudentsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetProgramStudentsQuery , Result<PaginationList<StudentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<StudentResponse>>> Handle(GetProgramStudentsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = StudentCacheKeys.List(request.ProgramId, filter.SearchValue, filter.PageNumber, filter.PageSize);
        var tags = StudentCacheKeys.Tags(request.ProgramId);

        var source = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () => _unitOfWork.Repository<StudentAcademicProgram>()
                .GetQueryable()
                .Where(x => x.AcademicProgramId == request.ProgramId && !x.Student.IsDeleted)
                .ApplySearch(filter.SearchValue, x => x.Student.Name, x => x.Student.StudentCode)
                .Select(x => new StudentResponse(
                    x.Student.Id,
                    x.Student.Name,
                    x.Student.StudentCode,
                    x.Student.NationalIdOrPassport,
                    x.Student.Gender
                    )
                ),
            cancellationToken: cancellationToken,
            tags: tags
            );


		var response = await PaginationList<StudentResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
