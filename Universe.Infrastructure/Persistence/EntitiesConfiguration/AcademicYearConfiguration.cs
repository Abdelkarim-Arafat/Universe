using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

internal class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
{
    public void Configure(EntityTypeBuilder<AcademicYear> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Semesters)
            .WithOne(x => x.AcademicYear)
            .HasForeignKey(x => x.AcademicYearId);

        builder.HasData(AcademicYearSeed.Years);
    }
}
