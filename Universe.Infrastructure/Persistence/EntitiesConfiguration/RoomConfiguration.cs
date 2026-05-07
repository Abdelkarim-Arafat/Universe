using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Enums;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.RoomNumber)
           .IsRequired();

        builder.Property(r => r.Capacity)
          .IsRequired();

        builder.Property(s => s.RoomType)
            .HasConversion(
                to => to.ToString(),
                from => Enum.Parse<RoomType>(from)
            )
            .HasMaxLength(50)
            .IsRequired(true);

        builder.HasIndex(r => new { r.RoomNumber, r.BuildingId })
            .IsUnique();

        builder.HasOne(r => r.Building)
            .WithMany(b => b.Rooms)
            .HasForeignKey(r => r.BuildingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(RoomSeed.Data);
    }
}
