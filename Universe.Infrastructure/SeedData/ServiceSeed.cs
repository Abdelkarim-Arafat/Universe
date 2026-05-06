using Universe.Core.Entities;

namespace Universe.Infrastructure.SeedData;

public static class ServiceSeed
{
    public static readonly Service[] Data =
    {
        new Service { Id = Guid.Parse("019df8a8-8616-75c5-a883-d313c0e6105e"), Name = "Transcript Request", Price = 100, Description = "Request official academic transcript.", IsActive = true, CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) },
        new Service { Id = Guid.Parse("019df8a8-8618-7a77-b23e-8622839dba17"), Name = "Enrollment Certificate", Price = 50, Description = "Issue enrollment certificate.", IsActive = true, CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) },
        new Service { Id = Guid.Parse("019df8a8-8618-7a77-b23e-8623a7995017"), Name = "Course Withdrawal", Price = 75, Description = "Withdraw from a registered course.", IsActive = true, CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) },
        new Service { Id = Guid.Parse("019df8a8-8618-7a77-b23e-8624951a0f61"), Name = "Regrade Request", Price = 60, Description = "Request re-evaluation of exam results.", IsActive = true, CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) },
        new Service { Id = Guid.Parse("019df8a8-8618-7a77-b23e-86258a38447f"), Name = "ID Replacement", Price = 40, Description = "Issue replacement student ID card.", IsActive = true, CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) },
        new Service { Id = Guid.Parse("019df8a8-8618-7a77-b23e-8626f39bba2e"), Name = "Graduation Certificate", Price = 150, Description = "Request graduation certificate.", IsActive = true, CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) },
        new Service { Id = Guid.Parse("019df8a8-8618-7a77-b23e-8627e2d629d7"), Name = "Course Registration Adjustment", Price = 80, Description = "Adjust course registration.", IsActive = true, CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) },
        new Service { Id = Guid.Parse("019df8a8-8618-7a77-b23e-8628f56ee1f8"), Name = "Late Registration", Price = 120, Description = "Register for courses after deadline.", IsActive = true, CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) }
    };
}