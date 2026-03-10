using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class TeachingSessionConfiguration : IEntityTypeConfiguration<TeachingSession>
{
    public void Configure(EntityTypeBuilder<TeachingSession> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Day)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.HasOne(x => x.Instructor)
            .WithMany(x => x.TeachingSessions)
            .HasForeignKey(x => x.InstructorId);

        builder.HasOne(x => x.Room)
            .WithMany(x => x.TeachingSessions)
            .HasForeignKey(x => x.RoomId);
    }
}
