using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.RoomTypeServices.Dtos;
using Universe.Application.RoomTypeServices.Queries.GetRoomType;

namespace Universe.Application.RoomTypeServices.Queries.GetAllTypes;

public class GetAllTypesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllTypesQuery, Result<List<RoomTypeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<RoomTypeResponse>>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.RoomTypeRepository.GetAllAsync(cancellationToken);

        var types = result.Value.Adapt<List<RoomTypeResponse>>();

        return Result.Success(types);
    }
}
