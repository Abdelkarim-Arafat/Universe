using System;
using System.Collections.Generic;
using Universe.Core.Entities;

namespace Universe.Infrastructure.SeedData;

public static class BuildingSeed
{
    public static readonly Building[] Data =
    {
        new Building
        {
            Id = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-4014a33a997f"),
            Name = "Main Administration Building",
            Code = "ADM01",
            CreatedAt = new DateTime(2026, 5, 6, 8, 0, 0, DateTimeKind.Utc)
        },
        new Building
        {
            Id = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-b67175b29c14"),
            Name = "Faculty of Computing",
            Code = "COMP",
            CreatedAt = new DateTime(2026, 5, 6, 8, 0, 0, DateTimeKind.Utc)
        },
        new Building
        {
            Id = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-ddcc7f909a82"),
            Name = "Scientific Laboratories",
            Code = "LABS",
            CreatedAt = new DateTime(2026, 5, 6, 8, 0, 0, DateTimeKind.Utc)
        },
        new Building
        {
            Id = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-0356789e840b"),
            Name = "Central Library",
            Code = "LIB",
            CreatedAt = new DateTime(2026, 5, 6, 8, 0, 0, DateTimeKind.Utc)
        }
    };
}