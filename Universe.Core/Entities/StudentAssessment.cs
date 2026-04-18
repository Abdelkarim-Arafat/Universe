using System.ComponentModel.DataAnnotations;
using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class StudentAssessment : BaseEntity
{
    public decimal? degree { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public Guid CourseOfferingAssessmentId { get; set; }
    public CourseOfferingAssessment CourseOfferingAssessment { get; set; } = default!;
}
