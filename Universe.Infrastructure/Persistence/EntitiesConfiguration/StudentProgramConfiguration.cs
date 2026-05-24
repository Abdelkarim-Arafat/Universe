using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

internal class StudentAcademicProgramConfiguration : IEntityTypeConfiguration<StudentAcademicProgram>
{
    public void Configure(EntityTypeBuilder<StudentAcademicProgram> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.AcademicProgramId });
    }
}
