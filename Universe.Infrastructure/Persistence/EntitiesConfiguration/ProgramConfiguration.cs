using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class AcademicProgramConfiguration : IEntityTypeConfiguration<AcademicProgram>
{
    public void Configure(EntityTypeBuilder<AcademicProgram> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Code).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Description).HasMaxLength(10000);

        builder.HasMany(p => p.StudyLoadRules)
            .WithOne(p => p.AcademicProgram)
            .HasForeignKey(p => p.AcademicProgramId);

        builder.HasMany(p => p.Levels)
            .WithOne(p => p.AcademicProgram)
            .HasForeignKey(p => p.AcademicProgramId);

        builder.HasMany(p => p.Grades)
            .WithOne(p => p.AcademicProgram)
            .HasForeignKey(p => p.AcademicProgramId);
    }
}
