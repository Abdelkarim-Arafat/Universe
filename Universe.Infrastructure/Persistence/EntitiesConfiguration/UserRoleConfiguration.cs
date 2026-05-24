using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        //Default Data
        builder.HasData(
            new IdentityUserRole<Guid>
            {
                UserId = DefaultUsers.SVNUId,
                RoleId = RoleSeed.Admin.Id
            },
            new IdentityUserRole<Guid>
            {
                UserId = DefaultUsers.AcademicAdvisingId,
                RoleId = RoleSeed.AcademicAdvising.Id
            });

        builder.HasData(UserRoleSeed.Data);
    }
}