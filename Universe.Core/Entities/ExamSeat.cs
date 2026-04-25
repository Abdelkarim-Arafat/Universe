using Universe.Core.Entities.Core;

namespace Universe.Core.Entities;
 
public class ExamSeat : BaseEntity
{
    public int SeatNumber { get; set; }
    public Guid StudentId { get; set; }
    public Guid ExamSessionId { get; set; }

    // Navigation Properties
    public Student Student { get; set; } = default!;
    public ExamCommittee ExamCommittee { get; set; } = default!;
}
