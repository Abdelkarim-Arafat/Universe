
using System.Runtime.InteropServices;

namespace Universe.Application.DepartmentServices.Commands.RemoveDepartment;

public class RemoveDepartmentCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveDepartmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if(department is null) return Result.Failure(DepartmentErrors.DepartmentNotFound);

        _unitOfWork.Repository<Department>().SoftDelete(department);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
