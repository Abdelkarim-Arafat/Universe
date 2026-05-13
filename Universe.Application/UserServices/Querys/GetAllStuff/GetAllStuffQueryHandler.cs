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

		var source = _unitOfWork.UserRepository
            .GetAllStaffAsync()
            .ApplySearch(filter.SearchValue , x => x.Name)
            .Select(x => new StuffResponse(
                x.Id.ToString(),
                x.Name
            ));

        var response = await PaginationList<StuffResponse>
            .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
