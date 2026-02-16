using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class CourseDepartmentConfiguration : IEntityTypeConfiguration<CourseDepartment>
{
    public void Configure(EntityTypeBuilder<CourseDepartment> builder)
    {
        builder.HasKey(cd => new { cd.CourseId, cd.DepartmentId });
    }
}
