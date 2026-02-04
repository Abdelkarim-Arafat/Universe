using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Infrastructure.SeedData;
using System;
using System.Collections.Generic;
using System.Text;

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
                RoleId = DefaultRoles.Admin.Id
            },
            new IdentityUserRole<Guid>
            {
                UserId = DefaultUsers.AcadimicAdvisingId,
                RoleId = DefaultRoles.AcadimicAdvising.Id
            });
    }
}