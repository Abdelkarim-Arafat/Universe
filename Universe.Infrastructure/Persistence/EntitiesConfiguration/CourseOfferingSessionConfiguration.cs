using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class CourseOfferingSessionConfiguration : IEntityTypeConfiguration<CourseOfferingSession>
{
    public void Configure(EntityTypeBuilder<CourseOfferingSession> builder)
    {
        builder.HasKey(x => new { x.CourseOfferingId, x.TeachingSessionId });
    }
}
