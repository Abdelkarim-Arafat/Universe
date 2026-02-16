using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities.Core;
using Universe.Core.Interfaces;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Infrastructure.Persistence;

internal class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public ApplicationDbContext _context => context;
    private readonly Dictionary<Type, object> _repositories = new();

    public IRoleRepository RoleRepository
        => field ??= new RoleRepository(_context);

    public IUserRepository UserRepository
        => field ??= new UserRepository(_context);

    public IDepartmentRepository DepartmentRepository
        => field ??= new DepartmentRepository(_context);

    public ICourseRepository CourseRepository
        => field ??= new CourseRepository(_context);


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