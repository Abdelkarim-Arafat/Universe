using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetAllStudents;

public class GetProgramStudentsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetProgramStudentsQuery, Result<PaginationList<StudentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<StudentResponse>>> Handle(GetProgramStudentsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = StudentCacheKeys.List(request.ProgramId, filter.SearchValue, filter.PageNumber, filter.PageSize);
        var tags = StudentCacheKeys.Tags(request.ProgramId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<StudentAcademicProgram>()
                     .GetQueryable()
                     .AsNoTracking()
                     .Where(x => x.AcademicProgramId == request.ProgramId &&
                        x.Currently && 
                        !x.Student.IsDeleted
                     );

                if (!string.IsNullOrEmpty(filter.SearchValue))
                {
                    query = query.Where(x =>
                        x.Student.Name.Contains(filter.SearchValue) ||
                        x.Student.StudentCode.Contains(filter.SearchValue));
                }

                var source = query.Select(x => new StudentResponse(
                    x.Student.Id,
                    x.Student.Name,
                    x.Student.StudentCode,
                    x.Student.NationalIdOrPassport,
                    x.Student.Gender
                    )
                );

                return await PaginationList<StudentResponse>
                        .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);
            },
             cancellationToken: cancellationToken,
             tags: tags
        );

        return Result.Success(response);
    }
}