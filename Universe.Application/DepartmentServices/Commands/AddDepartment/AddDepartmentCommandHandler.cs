using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Commands.AddDepartment;

public class AddDepartmentCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddDepartmentCommand, Result<DepartmentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DepartmentResponse>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.DepartmentRepository
            .IsExistAsync(request.CollegeId, request.Name, request.Code , null ,cancellationToken);

        if (isExist) return Result.Failure<DepartmentResponse>(DepartmentErrors.DepartmentAlreadyExists);

        var department = request.Adapt<Department>();

        await _unitOfWork.Repository<Department>().AddAsync(department, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(department.Adapt<DepartmentResponse>());
    }
}
