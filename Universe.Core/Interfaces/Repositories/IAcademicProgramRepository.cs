using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IAcademicProgramRepository
{
    Task<bool> IsExistAsync(Guid AcademicProgramId, string Name, string Code, Guid? excludeId, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Guid AcademicProgramId, CancellationToken cancellationToken);
    Task<AcademicProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProgramSchedule?> GetScheduleAsync(Guid ProgramId, Guid SemesterId, CancellationToken cancellationToken);
    Task<IEnumerable<AcademicProgram>> GetAllAsync(Guid AcademicProgramId, CancellationToken cancellationToken);
    Task<Guid?> GetStudentCurrentProgramIdAsync(Guid StudentId, CancellationToken cancellationToken);
}