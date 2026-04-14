using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AuthServices.AuthDtos;
using Universe.Application.CourseServices.Dtos;
using Universe.Application.UserServices.UserDtos;
using Universe.Core.Entities;
using Universe.Core.Interfaces;
using Universe.Infrastructure.SeedData;

namespace Universe.Application.UserServices.Querys.GetAllStuff;

public class GetAllStuffCommandHandler(
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<GetAllStuffCommand, Result<PaginationList<StuffResponse>>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<PaginationList<StuffResponse>>> Handle(GetAllStuffCommand request, CancellationToken cancellationToken)
    {
        var query = _userManager.Users
        .Where(x => x.CollegeId == request.CollegeId
            && !x.IsDeleted
            && x.UserRoles.Any(r => r.RoleId == DefaultRoles.Staff.Id
                      || r.RoleId == DefaultRoles.AcademicAdvising.Id));

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
