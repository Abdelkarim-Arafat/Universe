using Universe.Core.Entities;

namespace Universe.Infrastructure.SeedData;

public static class GradeSeed
{
    public static readonly List<Grade> Data = new();

    static GradeSeed()
    {
        var programIds = new[]
        {
            Guid.Parse("019df1d0-68a6-7696-9aa6-4014a33a997f"), // CS
            Guid.Parse("019df1d0-b671-75b2-9c14-69ce00131461"), // AI
            Guid.Parse("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), // IS
            Guid.Parse("019df1d1-0356-789e-840b-56d31396608a"), // SE
            Guid.Parse("019df1d1-2da1-78d2-adeb-84cb6b07a459")  // CY
        };

        foreach (var programId in programIds)
        {
            Data.AddRange(GenerateGradesForProgram(programId));
        }
    }

    private static IEnumerable<Grade> GenerateGradesForProgram(Guid programId)
    {
        var seedDate = new DateTime(2026, 5, 6, 8, 0, 0, DateTimeKind.Utc);

        return new List<Grade>
        {
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4b98fd3fe384"), Name = "Excellent High", Code = "A+", MinScore = 95, MaxScore = 100, MinGradePoint = 4.00m, MaxGradePoint = 4.00m, AcademicProgramId = programId, CreatedAt = seedDate},
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4b99731825a5"), Name = "Excellent",      Code = "A",  MinScore = 90, MaxScore = 94,  MinGradePoint = 3.70m, MaxGradePoint = 3.99m, AcademicProgramId = programId, CreatedAt = seedDate},
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4b9a8ed4c213"), Name = "Very Good High", Code = "B+", MinScore = 85, MaxScore = 89,  MinGradePoint = 3.30m, MaxGradePoint = 3.69m, AcademicProgramId = programId, CreatedAt = seedDate},
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4b9b4caa2568"), Name = "Very Good",      Code = "B",  MinScore = 80, MaxScore = 84,  MinGradePoint = 3.00m, MaxGradePoint = 3.29m, AcademicProgramId = programId, CreatedAt = seedDate},
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4b9cc785f14a"), Name = "Good High",      Code = "C+", MinScore = 75, MaxScore = 79,  MinGradePoint = 2.70m, MaxGradePoint = 2.99m, AcademicProgramId = programId, CreatedAt = seedDate},
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4b9dcedc1b35"), Name = "Good",           Code = "C",  MinScore = 70, MaxScore = 74,  MinGradePoint = 2.30m, MaxGradePoint = 2.69m, AcademicProgramId = programId, CreatedAt = seedDate},
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4b9ee24f9179"), Name = "Fair High",      Code = "D+", MinScore = 65, MaxScore = 69,  MinGradePoint = 2.00m, MaxGradePoint = 2.29m, AcademicProgramId = programId, CreatedAt = seedDate},
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4b9f01fa845c"), Name = "Fair",           Code = "D",  MinScore = 60, MaxScore = 64,  MinGradePoint = 1.00m, MaxGradePoint = 1.99m, AcademicProgramId = programId, CreatedAt = seedDate},
            new() { Id = Guid.Parse("019dfa31-42cc-7d42-82ad-4ba0d37d66eb"), Name = "Failed",         Code = "F",  MinScore = 0,  MaxScore = 59,  MinGradePoint = 0.00m, MaxGradePoint = 0.00m, AcademicProgramId = programId, CreatedAt = seedDate}
        };
    }
}