using Microsoft.EntityFrameworkCore;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

public class RoomRepository(ApplicationDbContext context) : IRoomRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> CheckValidRoomNumberAsync(Guid? Id, Guid BuildingId, int RoomNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
              .AnyAsync(room => room.RoomNumber == RoomNumber
              && room.BuildingId == BuildingId
              && !room.IsDeleted
              && ((Id == null) || (room.Id != Id)));
    }
    public async Task<bool> IsExistAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AnyAsync(room => room.Id == Id && !room.IsDeleted, cancellationToken);
    }

    public async Task<Room?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .FirstOrDefaultAsync(room => room.Id == Id && !room.IsDeleted, cancellationToken);
    }
    public async Task<List<Room>> GetAvailableRoomsForExamAsync
        (Guid buildingId, DateOnly date, TimeOnly startTime, TimeOnly endTime, CancellationToken cancellationToken)
    {
        return await _context.Rooms
            .Where(room => room.BuildingId == buildingId
        && !room.ExamCommittees
            .Any(committee => committee.CourseOfferingExam.Date == date
        && ((committee.CourseOfferingExam.StartTime < endTime && startTime < committee.CourseOfferingExam.EndTime))))
            .ToListAsync(cancellationToken);
    }
}
