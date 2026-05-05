using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AuthServices.AuthDtos;
using Universe.Core.Contracts.Course;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Entities;
using Universe.Core.Interfaces;

namespace Universe.Application.UserServices.Querys.GetAllStuff;

public class GetAllStuffCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAllStuffCommand, Result<PaginationList<StuffResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<StuffResponse>>> Handle(GetAllStuffCommand request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.UserRepository.GetAllStaffAsync();
        var filter = request.filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
        }

        if (!string.IsNullOrEmpty(filter.SortColumn))
        {
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");
        }

        var source = query.Select(x => new StuffResponse(
                x.Id.ToString(),
                x.Name
            ));

        var response = await PaginationList<StuffResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
