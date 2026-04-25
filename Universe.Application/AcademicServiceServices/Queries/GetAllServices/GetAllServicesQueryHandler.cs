using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicEventServices.EvenetDtos;

namespace Universe.Application.AcademicServiceServices.Queries.GetAllServices;

public class GetAllServicesQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAllServicesQuery, Result<PaginationList<ServiceResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<PaginationList<ServiceResponse>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Service>()
            .GetQueryable()
            .Where(x => x.CollegeId == request.CollegeId && !x.IsDeleted);

        var filter = request.FilterRequest;

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new ServiceResponse(
                x.Id.ToString(),
                x.Name,
                x.Description,
                x.Price
            ));

        var response = await PaginationList<ServiceResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }

}
