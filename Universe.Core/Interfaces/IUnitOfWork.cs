using Microsoft.EntityFrameworkCore.Storage;
using Universe.Core.Entities.Core;
using Universe.Core.Interfaces.Repositories;


namespace Universe.Core.Interfaces;

public interface IUnitOfWork: IAsyncDisposable
{
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
    IRoleRepository RoleRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IServiceRepository ServiceRepository { get; }
    ICourseOfferingRepository CourseOfferingRepository { get; }
    IAcademicProgramRepository AcademicProgramRepository { get; }
    IAcademicYearRepository AcademicYearRepository { get; }
    IAcademicEventRepository AcademicEventRepository { get; }
    ICourseRepository CourseRepository { get; }
    ISessionRepository SessionRepository { get; }
    IBuildingRepository BuildingRepository { get; }
    IRoomRepository RoomRepository { get; }
    ILevelRepository LevelRepository { get; }
    IGradeRepository GradeRepository { get; }
    IStudyLoadRuleRepository StudyLoadRuleRepository { get; }
    IStudyLoadByLevelRepository StudyLoadByLevelRepository { get; }
    IUserRepository UserRepository { get; }
    ICollegeRepository CollegeRepository { get; }
    IEnrollmentRepository EnrollmentRepository { get; }
    IStudentAssessmentRepository StudentAssessmentRepository { get; }
    IExamRepository ExamRepository { get; }
    Task<int> CompleteAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionIsolatedAsync(CancellationToken cancellationToken);
}
