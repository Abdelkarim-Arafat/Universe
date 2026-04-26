using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class CourseOfferingCommittee : BaseEntity
{
    public CourseOfferingCommittee() { Id = Guid.CreateVersion7(); }
    public Guid Id { get; set; }
    public Guid CourseOfferingExamId { get; set; }
    public Guid ExamCommitteeId { get; set; }
    // Navigation
    public CourseOfferingExam CourseOfferingExam { get; set; } = default!;
    public ExamCommittee ExamCommittee { get; set; } = default!;
    public ICollection<ExamSeat> ExamSeats { get; set; } = [];
}
