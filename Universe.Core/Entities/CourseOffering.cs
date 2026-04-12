
using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class CourseOffering : BaseEntity
{
    public Guid Id { get; set; }
    public CourseOffering(){ Id = Guid.CreateVersion7(); }
    public decimal CreditHours { get; set; }
    public decimal TotalGrade { get; set; }
    public decimal SuccessPercentage { get; set; }
    public bool IsOptional { get; set; }
    public bool IsIncludedInGpa { get; set; } = true;
    public string? OptionalGroupCode { get; set; }
    public bool IsOpenForControl { get; set; }
    public int NumberOfGroups { get; set; } = 1;
    public RequirementType Type { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;

    public Guid SemesterId { get; set; }
    public Semester Semester { get; set; } = default!;

    public Guid AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = default!;

    public Guid LevelId { get; set; }
    public Level Level { get; set; } = default!;

    public ICollection<CourseOfferingAssessment> Assessments { get; set; } = [];
    public ICollection<CourseOfferingSession> CourseOfferingSessions { get; set; } = [];
    public ICollection<Enrollment> Enrollments { get; set; } = [];

}