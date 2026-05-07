using System;
using System.Collections.Generic;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Infrastructure.SeedData;

public static class RoomSeed
{
    private static readonly DateTime SeedDate = new DateTime(2026, 5, 6, 14, 0, 0, DateTimeKind.Utc);

    public static readonly Room[] Data =
    {
        // === 1. Faculty of Computing (COMP) ===
        new Room
        {
            Id = Guid.Parse("019dfaa3-92f1-7d12-840b-4014a33a997f"),
            Name = "Steve Jobs Hall",
            RoomNumber = 101,
            Capacity = 200,
            RoomType = RoomType.LectureHall,
            BuildingId = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-b67175b29c14"),
            CreatedAt = SeedDate
        },
        new Room
        {
            Id = Guid.Parse("019dfaa3-92f1-7d12-840b-b67175b29c14"),
            Name = "Cyber Security Lab",
            RoomNumber = 302,
            Capacity = 25,
            RoomType = RoomType.ComputerLab,
            BuildingId = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-b67175b29c14"),
            CreatedAt = SeedDate
        },

        // === 2. Scientific Laboratories (LABS) ===
        new Room
        {
            Id = Guid.Parse("019dfaa3-92f1-7d12-840b-ddcc7f909a82"),
            Name = "Newton Physics Lab",
            RoomNumber = 10,
            Capacity = 30,
            RoomType = RoomType.ScientificLab,
            BuildingId = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-ddcc7f909a82"),
            CreatedAt = SeedDate
        },
        new Room
        {
            Id = Guid.Parse("019dfaa3-92f1-7d12-840b-0356789e840b"),
            Name = "Mechanical Workshop",
            RoomNumber = 15,
            Capacity = 40,
            RoomType = RoomType.Workshop,
            BuildingId = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-ddcc7f909a82"),
            CreatedAt = SeedDate
        },

        // === 3. Main Administration Building (ADM01) ===
        new Room
        {
            Id = Guid.Parse("019dfaa3-92f1-7d12-840b-2da178d2adeb"),
            Name = "Board of Directors Hall",
            RoomNumber = 500,
            Capacity = 60,
            RoomType = RoomType.ClassRoom, // باستخدام ClassRoom كمكان للاجتماعات
            BuildingId = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-4014a33a997f"),
            CreatedAt = SeedDate
        },

        // === 4. Central Library (LIB) ===
        new Room
        {
            Id = Guid.Parse("019dfaa3-92f1-7d12-840b-84cb6b07a459"),
            Name = "Quiet Study Zone",
            RoomNumber = 1,
            Capacity = 100,
            RoomType = RoomType.ClassRoom,
            BuildingId = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-0356789e840b"),
            CreatedAt = SeedDate
        },
        new Room
        {
            Id = Guid.Parse("019dfaa3-92f1-7d12-840b-a45984cb6b07"),
            Name = "E-Learning Center",
            RoomNumber = 2,
            Capacity = 45,
            RoomType = RoomType.LanguageLab,
            BuildingId = Guid.Parse("019dfa7b-3b1a-7b3a-9e1d-0356789e840b"),
            CreatedAt = SeedDate
        }
    };
}