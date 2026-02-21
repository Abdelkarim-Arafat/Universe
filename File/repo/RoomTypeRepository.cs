using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class RoomTypeRepository(ApplicationDbContext context) : IRoomTypeRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> CheckIfRoomTypeExist(Guid id, CancellationToken cancellationToken = default)
    {
        var isExist = await _context.RoomTypes.AnyAsync(type => type.Id == id && !type.IsDeleted, cancellationToken);
        if(!isExist)
            return Result.Failure(RoomErrors.RoomTypeNotFound);
        return Result.Success();
    }

    public async Task<Result<List<RoomType>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var types = await _context.RoomTypes.Where(type => !type.IsDeleted).ToListAsync(cancellationToken);
        return Result.Success(types);
    }
    public async Task<Result<RoomType>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.RoomTypes.FirstOrDefaultAsync(type => type.Id == id && !type.IsDeleted, cancellationToken)
            is RoomType type
            ? Result.Success(type)
            : Result.Failure<RoomType>(RoomErrors.RoomTypeNotFound);
    }

}
