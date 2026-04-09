using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class CourseOfferingConfiguration : IEntityTypeConfiguration<CourseOffering>
{
    public void Configure(EntityTypeBuilder<CourseOffering> builder)
    {
        builder.Property(co => co.OptionalGroupCode)
            .IsRequired(false)
            .HasMaxLength(50);


        builder.Property(co => co.CreditHours)
            .HasColumnType("decimal(5,2)");

        builder.Property(co => co.TotalGrade)
            .HasColumnType("decimal(5,2)");

        builder.Property(co => co.SuccessPercentage)
            .HasColumnType("decimal(5,2)");

        builder.Property(s => s.Type)
            .HasConversion(
                to => to.ToString(),
                from => Enum.Parse<RequirementType>(from)
            )
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(co => co.Semester)
            .WithMany(co => co.CourseOfferings)
            .HasForeignKey(co => co.SemesterId);

        builder.HasOne(co => co.AcademicProgram)
            .WithMany(co => co.CourseOfferings)
            .HasForeignKey(co => co.AcademicProgramId);

        builder.HasOne(co => co.Level)
            .WithMany(co => co.CourseOfferings)
            .HasForeignKey(co => co.LevelId);

        builder.HasMany(co => co.Assessments)
            .WithOne(a => a.CourseOffering)
            .HasForeignKey(a => a.CourseOfferingId);
    }
}