using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Commands.CreateRoomType;

public class CreateRoomTypeCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateRoomTypeCommand, Result<RoomTypeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<RoomTypeResponse>> Handle(CreateRoomTypeCommand command, CancellationToken cancellationToken)
    {
        var type = command.Adapt<RoomType>();
        await _unitOfWork.Repository<RoomType>().AddAsync(type , cancellationToken);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            return Result.Failure<RoomTypeResponse>(
                new Error("DatabaseError", "Failed to create roomtype", StatusCodes.Status409Conflict));
        }
        return Result.Success(type.Adapt<RoomTypeResponse>());
    }
}
