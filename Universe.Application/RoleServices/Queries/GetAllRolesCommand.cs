using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoleServices.RoleDtos;

namespace Universe.Application.RoleServices.Queries;

public record GetAllRolesCommand (
    string RoleName
) : IRequest<Result<List<RoleResponse>>>;
