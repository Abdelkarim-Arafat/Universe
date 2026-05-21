using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AuthServices.AuthDtos;
using Universe.Core.Contracts.Course;
using Universe.Core.Contracts.User;
using Universe.Core.Entities;
using Universe.Core.Interfaces;

namespace Universe.Application.UserServices.Querys.GetAllStuff;

public class GetAllStuffQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAllStuffQuery, Result<PaginationList<StuffResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<StuffResponse>>> Handle(GetAllStuffQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var query = _unitOfWork.UserRepository
            .GetAllStaffAsync();

        if (!string.IsNullOrEmpty(filter.SearchValue))
        {
            query = query.Where(x => x.Name.Contains(filter.SearchValue));
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