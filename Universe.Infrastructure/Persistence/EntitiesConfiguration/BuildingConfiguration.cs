using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
   
        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.Code).IsUnique();

     
        builder.Property(b => b.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(b => b.Name)
            .IsUnique();

        builder.HasMany(b => b.Rooms)
            .WithOne(r => r.Building)
            .HasForeignKey(r => r.BuildingId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasData(BuildingSeed.Data);
    }
}
