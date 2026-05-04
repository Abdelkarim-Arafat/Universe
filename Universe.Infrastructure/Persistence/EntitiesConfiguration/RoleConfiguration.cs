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
                Id = RoleSeed.Admin.Id,
                Name = RoleSeed.Admin.Name,
                NormalizedName = RoleSeed.Admin.Name.ToUpper(),
                ConcurrencyStamp = RoleSeed.Admin.ConcurrencyStamp,
                Level = RoleSeed.Admin.Level,
            },
            new ApplicationRole
            {
                Id = RoleSeed.AcademicAdvising.Id,
                Name = RoleSeed.AcademicAdvising.Name,
                NormalizedName = RoleSeed.AcademicAdvising.Name.ToUpper(),
                ConcurrencyStamp = RoleSeed.AcademicAdvising.ConcurrencyStamp,
                Level = RoleSeed.AcademicAdvising.Level
            },
            new ApplicationRole
            {
                Id = RoleSeed.Staff.Id,
                Name = RoleSeed.Staff.Name,
                NormalizedName = RoleSeed.Staff.Name.ToUpper(),
                ConcurrencyStamp = RoleSeed.Staff.ConcurrencyStamp,
                Level = RoleSeed.Staff.Level
            },
            new ApplicationRole
            {
                Id = RoleSeed.Student.Id,
                Name = RoleSeed.Student.Name,
                NormalizedName = RoleSeed.Student.Name.ToUpper(),
                ConcurrencyStamp = RoleSeed.Student.ConcurrencyStamp,
                Level = RoleSeed.Student.Level,
                IsDefault = true
            }
        ]);
    }
}