using System;
using System.Collections.Generic;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYears;
using Universe.Core.Contracts.AcademicProgram;
using Universe.Core.Contracts.AcadimicYearAndSemesters;


namespace Universe.Application.AcademicYearAndSemestersServices.Queries.GetAcademicYears;

public class GetAcademicYearsQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetAcademicYearsQuery, Result<PaginationList<AcademicYearResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<AcademicYearResponse>>> Handle(GetAcademicYearsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = AcademicYearCacheKeys.List(request.CollegeId, filter.SearchValue,
                        filter.SortColumn, filter.SortDirection, filter.PageNumber, filter.PageSize);

        var tags = AcademicYearCacheKeys.Tags(request.CollegeId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<AcademicYear>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(d => d.CollegeId == request.CollegeId && !d.IsDeleted)
                    .Select(x => new AcademicYearResponse(
                        x.Id,
                        x.Name
                        )
                    );

                if(!string.IsNullOrEmpty(filter.SearchValue))
                {
                    query = query.Where(x => x.Name.Contains(filter.SearchValue));
                }

                return await PaginationList<AcademicYearResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
            );

        return Result.Success(response);
    }
}
