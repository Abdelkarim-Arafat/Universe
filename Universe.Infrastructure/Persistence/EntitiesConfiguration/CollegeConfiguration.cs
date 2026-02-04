using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class CollegeConfiguration : IEntityTypeConfiguration<College>
{
    public void Configure(EntityTypeBuilder<College> builder)
    {
        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.HasMany(c => c.Users)
            .WithOne(u => u.College)
            .HasForeignKey(u => u.CollegeId);

        builder.HasData(
            new College
            {
                Id = DefaultCollege.Id,
                Name = DefaultCollege.Name
            }
        );
    }
}
