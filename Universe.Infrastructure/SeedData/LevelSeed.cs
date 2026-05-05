using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Infrastructure.SeedData;

public static class LevelSeed
{
    public static readonly Level[] Data =
    {
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cf36f1d180a"), Name = "Level 1", MinHours = 0,   MaxHours = 36,  AcademicProgramId = Guid.Parse("019df1d0-68a6-7696-9aa6-4014a33a997f"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cf4d268fbec"), Name = "Level 2", MinHours = 37,  MaxHours = 72,  AcademicProgramId = Guid.Parse("019df1d0-68a6-7696-9aa6-4014a33a997f"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cf568231cba"), Name = "Level 3", MinHours = 73,  MaxHours = 108, AcademicProgramId = Guid.Parse("019df1d0-68a6-7696-9aa6-4014a33a997f"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cf6124b1ecb"), Name = "Level 4", MinHours = 109, MaxHours = 144, AcademicProgramId = Guid.Parse("019df1d0-68a6-7696-9aa6-4014a33a997f"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cf7ad92b89e"), Name = "Level 1", MinHours = 0,   MaxHours = 36,  AcademicProgramId = Guid.Parse("019df1d0-b671-75b2-9c14-69ce00131461"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cf8937d84f4"), Name = "Level 2", MinHours = 37,  MaxHours = 72,  AcademicProgramId = Guid.Parse("019df1d0-b671-75b2-9c14-69ce00131461"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cf9e82c4b82"), Name = "Level 3", MinHours = 73,  MaxHours = 108, AcademicProgramId = Guid.Parse("019df1d0-b671-75b2-9c14-69ce00131461"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cfa9abd172d"), Name = "Level 4", MinHours = 109, MaxHours = 144, AcademicProgramId = Guid.Parse("019df1d0-b671-75b2-9c14-69ce00131461"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cfb22e7bb31"), Name = "Level 1", MinHours = 0,   MaxHours = 34,  AcademicProgramId = Guid.Parse("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cfcf91e0ee9"), Name = "Level 2", MinHours = 35,  MaxHours = 68,  AcademicProgramId = Guid.Parse("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cfdd431525b"), Name = "Level 3", MinHours = 69,  MaxHours = 102, AcademicProgramId = Guid.Parse("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cfeb8e8d41d"), Name = "Level 4", MinHours = 103, MaxHours = 138, AcademicProgramId = Guid.Parse("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7cffb491c63b"), Name = "Level 1", MinHours = 0,   MaxHours = 38,  AcademicProgramId = Guid.Parse("019df1d1-0356-789e-840b-56d31396608a"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7d00e6c3ddce"), Name = "Level 2", MinHours = 39,  MaxHours = 76,  AcademicProgramId = Guid.Parse("019df1d1-0356-789e-840b-56d31396608a"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7d01aebd87c1"), Name = "Level 3", MinHours = 77,  MaxHours = 114, AcademicProgramId = Guid.Parse("019df1d1-0356-789e-840b-56d31396608a"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7d024664404f"), Name = "Level 4", MinHours = 115, MaxHours = 150, AcademicProgramId = Guid.Parse("019df1d1-0356-789e-840b-56d31396608a"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7d033b7e618f"), Name = "Level 1", MinHours = 0,   MaxHours = 36,  AcademicProgramId = Guid.Parse("019df1d1-2da1-78d2-adeb-84cb6b07a459"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7d049703d7cb"), Name = "Level 2", MinHours = 37,  MaxHours = 72,  AcademicProgramId = Guid.Parse("019df1d1-2da1-78d2-adeb-84cb6b07a459"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7d05feaad5d1"), Name = "Level 3", MinHours = 73,  MaxHours = 108, AcademicProgramId = Guid.Parse("019df1d1-2da1-78d2-adeb-84cb6b07a459"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)},
        new Level { Id = Guid.Parse("019df4f8-4012-7469-9aa8-7d065b1fbceb"), Name = "Level 4", MinHours = 109, MaxHours = 144, AcademicProgramId = Guid.Parse("019df1d1-2da1-78d2-adeb-84cb6b07a459"), CreatedAt = new DateTime(2026,5,4,8,0,0,DateTimeKind.Utc)}
    };
}