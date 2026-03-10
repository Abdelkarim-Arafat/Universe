using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class ProgramScheduleConfiguration : IEntityTypeConfiguration<ProgramSchedule>
{
    public void Configure(EntityTypeBuilder<ProgramSchedule> builder)
    {
        builder.HasKey(x => new { x.ProgramId, x.SemesterId });

        builder.HasIndex(x => new { x.ProgramId, x.SemesterId });

        builder.Property(x => x.DayStartTime)
            .IsRequired();

        builder.Property(x => x.DayEndTime)
            .IsRequired();

        builder.Property(x => x.SlotDurationMinutes)
            .IsRequired();

        builder.HasOne(x => x.Program)
            .WithMany(x => x.ProgramSchedules)
            .HasForeignKey(x => x.ProgramId);

        builder.HasOne(x => x.Semester)
            .WithMany(x => x.ProgramSchedules)
            .HasForeignKey(x => x.SemesterId);

        builder.HasIndex(x => new { x.ProgramId, x.SemesterId })
            .IsUnique();
    }
}