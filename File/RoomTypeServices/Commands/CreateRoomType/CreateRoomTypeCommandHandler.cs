using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Commands.CreateRoomType;

public class CreateRoomTypeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRoomTypeCommand, Result<RoomTypeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomTypeResponse>> Handle(CreateRoomTypeCommand command, CancellationToken cancellationToken)
    {
        var building = command.Adapt<RoomType>();
        _unitOfWork.Repository<RoomType>().Add(building, cancellationToken);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {

            return Result.Failure<RoomTypeResponse>(
                new Error("DatabaseError", ex.Message, StatusCodes.Status409Conflict));
        }
        return Result.Success(building.Adapt<RoomTypeResponse>());
    }
}
