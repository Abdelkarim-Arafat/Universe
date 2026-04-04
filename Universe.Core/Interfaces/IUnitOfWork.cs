using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities.Core;
using Universe.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Interfaces;

public interface IUnitOfWork: IAsyncDisposable
{
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
    IRoleRepository RoleRepository { get; }
    ICourseOfferingRepository CourseOfferingRepository { get; }
    IAcademicProgramRepository AcademicProgramRepository { get; }
    IAcademicYearRepository AcademicYearRepository { get; }
    ICourseRepository CourseRepository { get; }
    ISessionRepository SessionRepository { get; }
    IBuildingRepository BuildingRepository { get; }
    IRoomRepository RoomRepository { get; }
    IRoomTypeRepository RoomTypeRepository { get; }
    ILevelRepository LevelRepository { get; }
    IGradeRepository GradeRepository { get; }
    IStudyLoadRuleRepository StudyLoadRuleRepository { get; }
    IUserRepository UserRepository { get; }
    ICollegeRepository CollegeRepository { get; }
    IEnrollmentRepository EnrollmentRepository { get; }
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}
