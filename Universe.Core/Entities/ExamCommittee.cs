using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;

public class ExamCommittee : BaseEntity
{ 
    public Guid Id { get; set; }
    public ExamCommittee() { Id = Guid.CreateVersion7(); }
    public int MaxCapacity { get; set; }
    public int CommitteeNumber { get; set; }
    public Guid CourseOfferingExamId { get; set; }
    public Guid RoomId { get; set; }

    // Navigation Properties
   
    public CourseOfferingExam CourseOfferingExam { get; set; } = default!;
    public Room Room { get; set; } = default!;
    public ICollection<ExamSeat> ExamSeats { get; set; } = [];
}