using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.DepartmentServices.DepartmentDtos;

namespace Universe.Application.DepartmentServices.Query.GetDepartments;

public class GetDepartmentsCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetDepartmentsCommand, Result<List<DepartmentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<DepartmentResponse>>> Handle(GetDepartmentsCommand request, CancellationToken cancellationToken)
    {
        var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(request.CollegeId, cancellationToken);

        return Result.Success(departments.Adapt<List<DepartmentResponse>>());
    }
}
