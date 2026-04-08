using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class StudentSemesterSummaryConfiguration : IEntityTypeConfiguration<StudentSemesterSummary>
{
    public void Configure(EntityTypeBuilder<StudentSemesterSummary> builder)
    {
        builder.HasIndex(s => s.StudentId);
    }
}
