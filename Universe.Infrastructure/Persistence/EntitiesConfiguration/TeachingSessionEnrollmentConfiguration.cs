using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

internal class TeachingSessionEnrollmentConfiguration : IEntityTypeConfiguration<TeachingSessionEnrollment>
{
    public void Configure(EntityTypeBuilder<TeachingSessionEnrollment> builder)
    {
        builder.HasKey(x => new { x.EnrollmentId, x.TeachingSessionId });
    }
}
