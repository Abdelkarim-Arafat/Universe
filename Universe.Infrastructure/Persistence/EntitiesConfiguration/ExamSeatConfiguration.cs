using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

public class ExamSeatConfiguration : IEntityTypeConfiguration<ExamSeat>
{
    public void Configure(EntityTypeBuilder<ExamSeat> builder)
    {
        builder.HasKey(es => new { es.StudentId, es.ExamSessionId });
    }
}
