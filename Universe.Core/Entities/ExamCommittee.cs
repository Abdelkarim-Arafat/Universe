using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;
 
public class ExamCommittee : BaseEntity
{ 
    public Guid Id { get; set; }
    public ExamCommittee() { Id = Guid.CreateVersion7(); }
    public int MaxCapacity { get; set; }
    public int CommitteeNumber { get; set; }
    public Guid RoomId { get; set; }
    public Guid ExamTermId { get; set; }
    
    // Navigation Properties
    public Room Room { get; set; } = default!;
    public ExamTerm ExamTerm { get; set; } = default!;
    public ICollection<CourseOfferingCommittee> CourseOfferingCommittees { get; set; } = [];
}