using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

internal class AcademicEventConfiguration : IEntityTypeConfiguration<AcademicEvent>
{
    public void Configure(EntityTypeBuilder<AcademicEvent> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(x => x.Program)
            .WithMany(x => x.AcademicEvents)
            .HasForeignKey(e => e.ProgramId);

        builder.HasOne(x => x.Semester)
            .WithMany(x => x.AcademicEvents)
            .HasForeignKey(e => e.SemesterId);

        builder.Property(s => s.Type)
            .HasConversion(
                to => to.ToString(),
                from => Enum.Parse<EventType>(from)
            )
            .HasMaxLength(500)
            .IsRequired();
    }
}
