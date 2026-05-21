using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicEventServices.EvenetDtos;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Queries.GetAllServices;


public class GetServicesQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
    ) : IRequestHandler<GetServicesQuery, Result<PaginationList<ServiceResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PaginationList<ServiceResponse>>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var cacheKey = ServiceCacheKeys.List(request.CollegeId, filter.SearchValue,
                        filter.SortColumn, filter.SortDirection, filter.PageNumber, filter.PageSize);

        var tags = ServiceCacheKeys.Tags(request.CollegeId);

        var response = await _cacheService.GetOrCreateAsync(
            key: cacheKey,
            factory: async () =>
            {
                var query = _unitOfWork.Repository<Service>()
                    .GetQueryable()
                    .AsNoTracking()
                    .Where(d => d.CollegeId == request.CollegeId && !d.IsDeleted)
                    .Select(x => new ServiceResponse (
                        x.Id,
                        x.Name,
                        x.Description,
                        x.Price
                        )
                    );

                if(!string.IsNullOrEmpty(filter.SearchValue))
                {
                    query = query.Where(x => x.Name.Contains(filter.SearchValue));
                }

                return await PaginationList<ServiceResponse>
                    .CreateAsync(query, filter.PageNumber, filter.PageSize, cancellationToken);
            },
            cancellationToken: cancellationToken,
            tags: tags
            );

        return Result.Success(response);
    }
}

