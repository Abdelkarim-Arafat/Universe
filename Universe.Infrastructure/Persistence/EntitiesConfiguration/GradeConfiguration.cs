using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
         
        builder.Property(c => c.Code).HasMaxLength(2).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.MinScore).IsRequired();
        builder.Property(c => c.MaxScore).IsRequired();

        builder.Property(c => c.MinGradePoint).HasPrecision(3, 2); 
        builder.Property(c => c.MaxGradePoint).HasPrecision(3, 2);

        builder.HasOne(x => x.AcademicProgram)
               .WithMany(x => x.Grades)
               .HasForeignKey(x => x.AcademicProgramId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(GradeSeed.Data);
    }
}
