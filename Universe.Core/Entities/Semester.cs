using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class Semester : BaseEntity
{
    public Guid Id { get; set; }
    public Semester(){ Id = Guid.CreateVersion7(); }
    public TermType Name { get; set; }
    public Guid AcademicYearId { get; set; }
    public AcademicYear AcademicYear { get; set; } = default!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public bool IsResultAnnounced {  get; set; }
    public ICollection<CourseOffering> CourseOfferings { get; set; } = [];
    public ICollection<ProgramSchedule> ProgramSchedules { get; set; } = [];
    public ICollection<StudyLoadByLevel> StudyLoadByLevels { get; set; } = [];
    public ICollection<ExamTerm> ExamTerms { get; set; } = [];
}
