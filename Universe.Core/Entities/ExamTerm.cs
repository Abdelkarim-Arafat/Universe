using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;
 
public class ExamTerm : BaseEntity
{
    public Guid Id { get; set; } 
    public ExamTerm() { Id = Guid.CreateVersion7(); }
    public ExamType ExamType { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool IsPublished { get; set; }
    public Guid SemesterId { get; set; }
    public Guid AcademicProgramId { get; set; }

    // Navigation Property
    public AcademicProgram AcademicProgram { get; set; } = default!;
    public Semester Semester { get; set; } = default!;
    public ICollection<CourseOfferingExam> CourseOfferingExams { get; set; } = [];
}
