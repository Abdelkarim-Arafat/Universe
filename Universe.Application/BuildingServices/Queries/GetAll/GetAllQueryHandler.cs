using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Queries.GetAll;

public class GetAllQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllQuery, Result<List<BuildingResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<BuildingResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.BuildingRepository.GetAllAsync(cancellationToken);
        return result.IsSuccess
            ? Result.Success(result.Value.Adapt<List<BuildingResponse>>())
            : Result.Failure<List<BuildingResponse>>(result.Error);
    }
}
