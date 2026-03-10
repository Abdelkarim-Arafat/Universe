using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities.Core;
using Universe.Core.Interfaces;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Infrastructure.Persistence;

internal class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public ApplicationDbContext _context => context;
    private readonly Dictionary<Type, object> _repositories = new();

    public IRoleRepository RoleRepository
        => field ??= new RoleRepository(_context);

    public IUserRepository UserRepository
        => field ??= new UserRepository(_context);

    public ICourseOfferingRepository CourseOfferingRepository
        => field ??= new CourseOfferingRepository(_context);

    public IAcademicProgramRepository AcademicProgramRepository
        => field ??= new AcademicProgramRepository(_context);

    public ICourseRepository CourseRepository
        => field ??= new CourseRepository(_context);

    public IBuildingRepository BuildingRepository
        => field ??= new BuildingRepository(_context);

    public IRoomRepository RoomRepository
        => field ??= new RoomRepository(_context);

    public IRoomTypeRepository RoomTypeRepository
        => field ??= new RoomTypeRepository(_context);

    public ILevelRepository LevelRepository
        => field ??= new LevelRepository(_context);

    public IStudyLoadRuleRepository StudyLoadRuleRepository
        => field ??= new StudyLoadRuleRepository(_context);

    public IAcademicYearRepository AcademicYearRepository
        => field ??= new AcademicYearRepository(_context);

    public IGradeRepository GradeRepository
        => field ??= new GradeRepository(_context);
    public ICollegeRepository CollegeRepository
        => field ??= new CollegeRepository(_context);

    public IStudyLoadByLevelRepository StudyLoadByLevelRepository
        => field ??= new StudyLoadByLevelRepository(_context);

    public ISessionRepository SessionRepository
        => field ??= new SessionRepository(_context);
    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);

    public ValueTask DisposeAsync() => _context.DisposeAsync();

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        var type = typeof(T);
        if(!_repositories.ContainsKey(type))
        {
            _repositories[type] = new GenericRepository<T>(_context);
        }
        return (IGenericRepository<T>)_repositories[type];
    }
}