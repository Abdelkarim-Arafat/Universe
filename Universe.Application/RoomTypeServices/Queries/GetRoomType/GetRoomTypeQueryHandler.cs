using Universe.Application.RoomTypeServices.Dtos;

namespace Universe.Application.RoomTypeServices.Queries.GetRoomType;

public class GetRoomTypeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoomTypeQuery, Result<RoomTypeResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<RoomTypeResponse>> Handle(GetRoomTypeQuery command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.RoomTypeRepository.GetByIdAsync(command.Id, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<RoomTypeResponse>(result.Error);

        var type = result.Value;
        return Result.Success(type.Adapt<RoomTypeResponse>());
    }
}
