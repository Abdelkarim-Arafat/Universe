using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Enums;

namespace Universe.Infrastructure.SeedData;


public static class AcademicProgramSeed
{
    public static readonly AcademicProgram[] Data =
    {
        new AcademicProgram
        {
            Id = Guid.Parse("019df1d0-68a6-7696-9aa6-4014a33a997f"),
            Name = "Computer Science",
            Code = "CS",
            Description = "Study of computation, algorithms, and software systems.",
            RequiredCreditHours = 144,
            CertificateTitle = "Bachelor of Computer Science",
            AcademicDegree = AcademicDegree.Bachelor,
            AcademicLoad = AcademicLoad.StudyLevel,
            CollegeId = CollegeSeed.Id,
            CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)
        },
        new AcademicProgram
        {
            Id = Guid.Parse("019df1d0-b671-75b2-9c14-69ce00131461"),
            Name = "Artificial Intelligence",
            Code = "AI",
            Description = "Study of intelligent systems, machine learning, and data science.",
            RequiredCreditHours = 144,
            CertificateTitle = "Bachelor of Artificial Intelligence",
            AcademicDegree = AcademicDegree.Bachelor,
            AcademicLoad = AcademicLoad.StudyLevel,
            CollegeId = CollegeSeed.Id,
            CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)
        },
        new AcademicProgram
        {
            Id = Guid.Parse("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"),
            Name = "Information Systems",
            Code = "IS",
            Description = "Focus on information management and business systems.",
            RequiredCreditHours = 138,
            CertificateTitle = "Bachelor of Information Systems",
            AcademicDegree = AcademicDegree.Bachelor,
            AcademicLoad = AcademicLoad.StudyLevel,
            CollegeId = CollegeSeed.Id,
            CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)
        },
        new AcademicProgram
        {
            Id = Guid.Parse("019df1d1-0356-789e-840b-56d31396608a"),
            Name = "Software Engineering",
            Code = "SE",
            Description = "Engineering principles applied to software development.",
            RequiredCreditHours = 150,
            CertificateTitle = "Bachelor of Software Engineering",
            AcademicDegree = AcademicDegree.Bachelor,
            AcademicLoad = AcademicLoad.StudyLevel,
            CollegeId = CollegeSeed.Id,
            CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc) 
        },
        new AcademicProgram
        {
            Id = Guid.Parse("019df1d1-2da1-78d2-adeb-84cb6b07a459"),
            Name = "Cyber Security",
            Code = "CY",
            Description = "Protection of systems, networks, and data from cyber threats.",
            RequiredCreditHours = 144,
            CertificateTitle = "Bachelor of Cyber Security",
            AcademicDegree = AcademicDegree.Bachelor,
            AcademicLoad = AcademicLoad.StudyLevel,
            CollegeId = CollegeSeed.Id,
            CreatedAt = new DateTime(2026, 5, 4, 8, 0, 0, DateTimeKind.Utc)
        }
    };
}
