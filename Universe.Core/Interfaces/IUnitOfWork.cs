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
    IUserRepository Userepository { get; }
    Task<int> CompleteAsync();
}
