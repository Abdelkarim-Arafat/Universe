using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context = context;
    public async Task AddAsync(T entity , CancellationToken cancellationToken)
        => await _context.Set<T>().AddAsync(entity , cancellationToken);

    public async Task AddRangeAsync(IEnumerable<T> entity , CancellationToken cancellationToken)
        => await _context.AddRangeAsync(entity , cancellationToken);
    public IQueryable<T> GetQueryable()
        => _context.Set<T>().Where(x => !x.IsDeleted).AsQueryable();

    public async Task<int> CountAsync(T entity) => await _context.Set<T>().CountAsync(e => !e.IsDeleted);
    public void DeletePermanently(T entity) => _context.Set<T>().Remove(entity);
    public void DeletePermanentlyRange(IEnumerable<T> entities) => _context.Set<T>().RemoveRange(entities);
    public void SoftDelete(T entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        Update(entity);
    }
    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Set<T>().Update(entity);
    }

    public async Task<IDbContextTransaction> BeginTransactionIsolatedAsync(CancellationToken cancellationToken)
    {
       return  await _context.Database
        .BeginTransactionAsync(System.Data.IsolationLevel.Serializable, cancellationToken);
    }
}
