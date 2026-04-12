using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;
using Universe.Application.RoomTypeServices.Dtos;
using Universe.Application.RoomTypeServices.Queries.GetRoomType;

namespace Universe.Application.RoomTypeServices.Queries.GetAllTypes;

public class GetAllTypesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllTypesQuery, Result<PaginationList<RoomTypeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<RoomTypeResponse>>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<RoomType>().GetQueryable();

        var filter = request.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new RoomTypeResponse
        (
             x.Id,
             x.Name
        ));

        var response = await PaginationList<RoomTypeResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
