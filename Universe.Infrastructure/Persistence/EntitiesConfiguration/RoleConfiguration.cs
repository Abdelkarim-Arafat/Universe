using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        //Default Data
        builder.HasData([
            new ApplicationRole
            {
                Id = DefaultRoles.Admin.Id,
                Name = DefaultRoles.Admin.Name,
                NormalizedName = DefaultRoles.Admin.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp
            },
            new ApplicationRole
            {
                Id = DefaultRoles.AcademicAdvising.Id,
                Name = DefaultRoles.AcademicAdvising.Name,
                NormalizedName = DefaultRoles.AcademicAdvising.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AcademicAdvising.ConcurrencyStamp,
            },
            new ApplicationRole
            {
                Id = DefaultRoles.Staff.Id,
                Name = DefaultRoles.Staff.Name,
                NormalizedName = DefaultRoles.Staff.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Staff.ConcurrencyStamp,
            },
            new ApplicationRole
            {
                Id = DefaultRoles.Student.Id,
                Name = DefaultRoles.Student.Name,
                NormalizedName = DefaultRoles.Student.Name.ToUpper(),
                ConcurrencyStamp = DefaultRoles.Student.ConcurrencyStamp,
                IsDefault = true
            }
        ]);
    }
}