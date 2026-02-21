using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.BuildingServices.Commands.CreateBuilding;
using Universe.Application.BuildingServices.Dtos;

namespace Universe.Application.BuildingServices.Commands.UpdateBuilding;

public class UpdateBuildingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBuildingCommand, Result<BuildingResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<BuildingResponse>> Handle(UpdateBuildingCommand command, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.BuildingRepository.GetByIdAsync(command.Id, cancellationToken);
        if (result.IsFailure)
            return Result.Failure<BuildingResponse>(BuildingErrors.NotFound);

        var building = result.Value;
        building.Name = command.Name;
        building.Code = command.Code;

        _unitOfWork.Repository<Building>().Update(building);

        try
        {
            await _unitOfWork.CompleteAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {

            return Result.Failure<BuildingResponse>(
                new Error("DatabaseError", ex.Message, StatusCodes.Status409Conflict));
        }
        return Result.Success(building.Adapt<BuildingResponse>());
    }
}

 
