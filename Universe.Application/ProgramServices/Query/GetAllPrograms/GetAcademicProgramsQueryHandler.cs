using System;
using System.Collections.Generic;
using Universe.Core.Contracts.AcademicProgram;


namespace Universe.Application.AcademicProgramServices.Query.GetAcademicPrograms;

public class GetAcademicProgramsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetAcademicProgramsQuery, Result<PaginationList<GetAcademicProgramsResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<GetAcademicProgramsResponse>>> Handle(GetAcademicProgramsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = AcademicProgramCacheKeys.List(request.CollegeId, filter.SearchValue,
                        filter.SortColumn, filter.SortDirection, filter.PageNumber, filter.PageSize);

        var tags = AcademicProgramCacheKeys.Tags(request.CollegeId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<AcademicProgram>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(d => d.CollegeId == request.CollegeId && !d.IsDeleted)
                    .Select(x => new GetAcademicProgramsResponse(
                        x.Id,
                        x.Name,
                        x.Code
                        )
                    );

                if(!string.IsNullOrEmpty(filter.SearchValue))
                {
                    query = query.Where(x => x.Name.Contains(filter.SearchValue) ||
                                        x.Code.Contains(filter.SearchValue));
                }

                return await PaginationList<GetAcademicProgramsResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
            );

        return Result.Success(response);
    }
}
