using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class StudentAssessmentConfiguration : IEntityTypeConfiguration<StudentAssessment>
{
    public void Configure(EntityTypeBuilder<StudentAssessment> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.CourseOfferingAssessmentId });
    }
}
