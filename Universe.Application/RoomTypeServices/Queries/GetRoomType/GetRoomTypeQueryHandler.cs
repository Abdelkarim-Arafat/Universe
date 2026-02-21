using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Queries.GetRoomType;

public class GetRoomTypeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoomTypeQuery, Result<RoomTypeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomTypeResponse>> Handle(GetRoomTypeQuery command, CancellationToken cancellationToken)
    {
        var type = await _unitOfWork.RoomTypeRepository.GetByIdAsync(command.Id, cancellationToken);
        if (type is null)
            return Result.Failure<RoomTypeResponse>(RoomErrors.RoomTypeNotFound);

        return Result.Success(type.Adapt<RoomTypeResponse>());
    }
}
