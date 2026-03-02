
using System.Runtime.InteropServices;

namespace Universe.Application.AcademicProgramServices.Commands.RemoveAcademicProgram;

public class RemoveAcademicProgramCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveAcademicProgramCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemoveAcademicProgramCommand request, CancellationToken cancellationToken)
    {
        var AcademicProgram = await _unitOfWork.AcademicProgramRepository.GetByIdAsync(request.Id, cancellationToken);
        if(AcademicProgram is null) return Result.Failure(AcademicProgramErrors.AcademicProgramNotFound);

        _unitOfWork.Repository<AcademicProgram>().SoftDelete(AcademicProgram);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}
