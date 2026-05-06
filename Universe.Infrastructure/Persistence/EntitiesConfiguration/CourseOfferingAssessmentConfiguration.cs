using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Infrastructure.SeedData;

namespace Universe.Infrastructure.Persistence.EntitiesConfiguration;

internal class CourseOfferingAssessmentConfiguration : IEntityTypeConfiguration<CourseOfferingAssessment>
{
    public void Configure(EntityTypeBuilder<CourseOfferingAssessment> builder)
    {
        builder.HasData(CourseOfferingSeed.assessments);
    }
}
