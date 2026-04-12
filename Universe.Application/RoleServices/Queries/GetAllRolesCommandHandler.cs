using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoleServices.RoleDtos;
using Universe.Core.Enums;

namespace Universe.Application.RoleServices.Queries;

public class GetAllRolesCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRolesCommand, Result<List<RoleResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.RoleRepository
            .GetRoleByNameAsync(request.RoleName, cancellationToken) is not { } role
            ) return Result.Failure<List<RoleResponse>>(RoleErrors.NotFound);

        var roles = await _unitOfWork.RoleRepository
            .GetAllRolesLessThanOrEqualAsync(role.Level , cancellationToken);

        return Result.Success(roles.Adapt<List<RoleResponse>>());
    }
}
