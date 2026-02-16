using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateDepartmentCommand, Result<DepartmentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DepartmentResponse>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {

        if (await _unitOfWork.DepartmentRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code, request.Id, cancellationToken))
        {
            return Result.Failure<DepartmentResponse>(DepartmentErrors.DepartmentAlreadyExists);
        }
             

        var department = await _unitOfWork.DepartmentRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if(department is null) return Result.Failure<DepartmentResponse>(DepartmentErrors.DepartmentNotFound);

        request.Adapt(department);

        _unitOfWork.Repository<Department>().Update(department);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(department.Adapt<DepartmentResponse>());
    }
}
