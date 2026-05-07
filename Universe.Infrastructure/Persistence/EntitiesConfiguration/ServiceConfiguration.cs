using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.College)
            .WithMany(c => c.Services)
            .HasForeignKey(s => s.CollegeId);

        builder.HasData(ServiceSeed.Data);
    }
}
