
using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class TeachingSession : BaseEntity
{
    public Guid Id { get; set; }
    public TeachingSession() { Id = Guid.CreateVersion7(); }
    public SessionType Type { get; set; }
    public Enums.DayOfWeek Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int GroupNumber { get; set; }
    public Guid InstructorId { get; set; }
    public ApplicationUser Instructor { get; set; } = default!;
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = default!;
    public ICollection<CourseOfferingSession> CourseOfferingSessions { get; set; } = [];
}