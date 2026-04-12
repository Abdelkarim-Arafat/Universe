using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IRoleRepository
{
    //Task<IEnumerable<string?>> GetRolePermissionsAsync(string roleId, CancellationToken cancellationToken);
    Task<IEnumerable<string>> GetUserPermissionsAsync(IEnumerable<string> userRoles, CancellationToken cancellationToken);
    Task<List<ApplicationRole>> GetAllRolesLessThanOrEqualAsync(int level, CancellationToken cancellationToken);
    Task<ApplicationRole?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken);
    //Task AddPermissionsAsync(IEnumerable<IdentityRoleClaim<string>> permissions, CancellationToken cancellationToken);
    //Task RemovePermissionsAsync(string roleId, IEnumerable<string> permissionsToRemove, CancellationToken cancellationToken);
}