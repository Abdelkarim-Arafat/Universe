using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Infrastructure.SeedData;

public static class AcademicYearSeed
{
    public static readonly AcademicYear[] Years =
    {
        new AcademicYear { Id = Guid.Parse("019df776-1f8f-76e6-abf8-02c380bbdf4f"), Name = "2020-2021", StartDate = new DateOnly(2020, 9, 1), EndDate = new DateOnly(2021, 8, 31), CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)  },
        new AcademicYear { Id = Guid.Parse("019df776-1f8f-76e6-abf8-02c488f9b604"), Name = "2021-2022", StartDate = new DateOnly(2021, 9, 1), EndDate = new DateOnly(2022, 8, 31), CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)  },
        new AcademicYear { Id = Guid.Parse("019df776-1f8f-76e6-abf8-02c591133e7d"), Name = "2022-2023", StartDate = new DateOnly(2022, 9, 1), EndDate = new DateOnly(2023, 8, 31), CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)  },
        new AcademicYear { Id = Guid.Parse("019df776-1f8f-76e6-abf8-02c6d020e3cf"), Name = "2023-2024", StartDate = new DateOnly(2023, 9, 1), EndDate = new DateOnly(2024, 8, 31), CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)  },
        new AcademicYear { Id = Guid.Parse("019df776-1f8f-76e6-abf8-02c790dab613"), Name = "2024-2025", StartDate = new DateOnly(2024, 9, 1), EndDate = new DateOnly(2025, 8, 31), CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)  },
        new AcademicYear { Id = Guid.Parse("019df776-1f8f-76e6-abf8-02c8c484f18c"), Name = "2025-2026", StartDate = new DateOnly(2025, 9, 1), EndDate = new DateOnly(2026, 8, 31), CollegeId = CollegeSeed.Id, CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)  }
    };

    public static readonly Semester[] Semesters =
    {
        // 2020-2021
        new Semester { Id = Guid.Parse("019df776-9108-75b2-9389-ac1049b0159f"), Name = TermType.Fall,   AcademicYearId = Years[0].Id, StartDate = new DateOnly(2020, 9, 1),  EndDate = new DateOnly(2021, 1, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },
        new Semester { Id = Guid.Parse("019df776-9108-75b2-9389-ac110706f6d0"), Name = TermType.Spring, AcademicYearId = Years[0].Id, StartDate = new DateOnly(2021, 2, 1),  EndDate = new DateOnly(2021, 6, 30), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = true, IsResultAnnounced = true },
        new Semester { Id = Guid.Parse("019df776-9108-75b2-9389-ac1257cd67fe"), Name = TermType.Summer, AcademicYearId = Years[0].Id, StartDate = new DateOnly(2021, 7, 1),  EndDate = new DateOnly(2021, 8, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },

        // 2021-2022
        new Semester { Id = Guid.Parse("019df776-bc77-7ca8-a0f6-3c3845c23a04"), Name = TermType.Fall,   AcademicYearId = Years[1].Id, StartDate = new DateOnly(2021, 9, 1),  EndDate = new DateOnly(2022, 1, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = true, IsResultAnnounced = true },
        new Semester { Id = Guid.Parse("019df776-bc77-7ca8-a0f6-3c39222b791a"), Name = TermType.Spring, AcademicYearId = Years[1].Id, StartDate = new DateOnly(2022, 2, 1),  EndDate = new DateOnly(2022, 6, 30), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },
        new Semester { Id = Guid.Parse("019df776-bc77-7ca8-a0f6-3c3a1ab74d05"), Name = TermType.Summer, AcademicYearId = Years[1].Id, StartDate = new DateOnly(2022, 7, 1),  EndDate = new DateOnly(2022, 8, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },

        // 2022-2023
        new Semester { Id = Guid.Parse("019df776-f240-7e2a-bef0-a153c6a90cdc"), Name = TermType.Fall,   AcademicYearId = Years[2].Id, StartDate = new DateOnly(2022, 9, 1),  EndDate = new DateOnly(2023, 1, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },
        new Semester { Id = Guid.Parse("019df776-f240-7e2a-bef0-a1549332c599"), Name = TermType.Spring, AcademicYearId = Years[2].Id, StartDate = new DateOnly(2023, 2, 1),  EndDate = new DateOnly(2023, 6, 30), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },
        new Semester { Id = Guid.Parse("019df776-f240-7e2a-bef0-a1557960866d"), Name = TermType.Summer, AcademicYearId = Years[2].Id, StartDate = new DateOnly(2023, 7, 1),  EndDate = new DateOnly(2023, 8, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },

        // 2023-2024
        new Semester { Id = Guid.Parse("019df777-2097-7f6a-84f3-97e02fa63d59"), Name = TermType.Fall,   AcademicYearId = Years[3].Id, StartDate = new DateOnly(2023, 9, 1),  EndDate = new DateOnly(2024, 1, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },
        new Semester { Id = Guid.Parse("019df777-2097-7f6a-84f3-97e1d0a5750b"), Name = TermType.Spring, AcademicYearId = Years[3].Id, StartDate = new DateOnly(2024, 2, 1),  EndDate = new DateOnly(2024, 6, 30), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = true },
        new Semester { Id = Guid.Parse("019df777-2097-7f6a-84f3-97e210d2106a"), Name = TermType.Summer, AcademicYearId = Years[3].Id, StartDate = new DateOnly(2024, 7, 1),  EndDate = new DateOnly(2024, 8, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = true, IsResultAnnounced = true },

        // 2024-2025
        new Semester { Id = Guid.Parse("019df777-504a-712e-b906-df4fabc6b69e"), Name = TermType.Fall,   AcademicYearId = Years[4].Id, StartDate = new DateOnly(2024, 9, 1),  EndDate = new DateOnly(2025, 1, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = false },
        new Semester { Id = Guid.Parse("019df777-504a-712e-b906-df50def24feb"), Name = TermType.Spring, AcademicYearId = Years[4].Id, StartDate = new DateOnly(2025, 2, 1),  EndDate = new DateOnly(2025, 6, 30), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = true, IsResultAnnounced = false },
        new Semester { Id = Guid.Parse("019df777-504a-712e-b906-df51d6c28ef4"), Name = TermType.Summer, AcademicYearId = Years[4].Id, StartDate = new DateOnly(2025, 7, 1),  EndDate = new DateOnly(2025, 8, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = false },

        // 2025-2026
        new Semester { Id = Guid.Parse("019df777-7a6a-7c4b-af7e-6a7903807bf7"), Name = TermType.Fall,   AcademicYearId = Years[5].Id, StartDate = new DateOnly(2025, 9, 1),  EndDate = new DateOnly(2026, 1, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = false },
        new Semester { Id = Guid.Parse("019df777-7a6a-7c4b-af7e-6a7affd69cb7"), Name = TermType.Spring, AcademicYearId = Years[5].Id, StartDate = new DateOnly(2026, 2, 1),  EndDate = new DateOnly(2026, 6, 30), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = true,  IsResultAnnounced = false },
        new Semester { Id = Guid.Parse("019df777-7a6a-7c4b-af7e-6a7b7b27fa44"), Name = TermType.Summer, AcademicYearId = Years[5].Id, StartDate = new DateOnly(2026, 7, 1),  EndDate = new DateOnly(2026, 8, 31), CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) , IsCurrent = false, IsResultAnnounced = false }
    };
}