using Universe.Core.Entities.Core;
namespace Universe.Core.Entities;

public class CourseOfferingExam : BaseEntity
{
    public Guid Id { get; set; }
    public CourseOfferingExam() { Id = Guid.CreateVersion7(); }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid CourseOfferingId { get; set; }
    public Guid ExamTermId { get; set; }

    // Navigation Properties
    public ExamTerm ExamTerm { get; set; } = default!;
    public CourseOffering CourseOffering { get; set; } = default!;
    public ICollection<CourseOfferingCommittee> CourseOfferingCommittees { get; set; } = [];
}
 