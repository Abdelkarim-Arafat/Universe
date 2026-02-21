using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Universe.Core.Entities;
 
namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.Property(c => c.Code)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.MinScore)
            .IsRequired();
        builder.Property(c => c.MaxScore)
            .IsRequired();
    }
}
