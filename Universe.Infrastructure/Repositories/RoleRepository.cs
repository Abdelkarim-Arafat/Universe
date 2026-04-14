using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class RoleRepository(
    ApplicationDbContext context
    ) : IRoleRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<string>> GetUserPermissionsAsync(IEnumerable<string> userRoles, CancellationToken cancellationToken) 
        => await _context.Roles
                .Join(_context.RoleClaims,
                    role => role.Id,
                    claim => claim.RoleId,
                    (role, claim) => new { role, claim }
                )
                .Where(x => userRoles.Contains(x.role.Name!))
                .Select(x => x.claim.ClaimValue!)
                .Distinct()
                .ToListAsync(cancellationToken);

    public async Task<List<ApplicationRole>> GetAllRolesLessThanOrEqualAsync(int level , CancellationToken cancellationToken)
        => await _context.Roles
            .Where(x => x.Level >= level)
            .ToListAsync(cancellationToken);

    public async Task<ApplicationRole?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken)
        => await _context.Roles
            .FirstOrDefaultAsync(x => x.Name == roleName, cancellationToken);
}
