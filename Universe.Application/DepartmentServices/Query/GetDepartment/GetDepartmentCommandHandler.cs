using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Query.GetDepartment;

public class GetDepartmentCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetDepartmentCommand , Result<DepartmentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DepartmentResponse>> Handle(GetDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(request.Id, cancellationToken);

        return department is not null
            ? Result.Success(department.Adapt<DepartmentResponse>())
            : Result.Failure<DepartmentResponse>(DepartmentErrors.DepartmentNotFound);
    }
}
