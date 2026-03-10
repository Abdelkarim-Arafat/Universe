using Universe.Core.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Interfaces.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity , CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken);
    void Update(T entity);
    void SoftDelete(T entity);
    IQueryable<T> GetQueryable();
    Task<int> CountAsync(T entity);
    void DeletePermanently(T entity);
    void DeletePermanentlyRange(IEnumerable<T> entities);
}
