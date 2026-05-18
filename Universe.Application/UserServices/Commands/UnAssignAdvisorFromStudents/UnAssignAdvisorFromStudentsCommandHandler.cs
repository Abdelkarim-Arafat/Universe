using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UnAssignAdvisorFromStudents;

public class UnAssignAdvisorFromStudentsCommandHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<UnAssignAdvisorFromStudentsCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UnAssignAdvisorFromStudentsCommand request, CancellationToken cancellationToken)
    {
        var students = await _unitOfWork.UserRepository
            .GetStudentsByIdsAsync(request.StudentIds, cancellationToken);

        if (students.Count != request.StudentIds.Count)
            return Result.Failure(StudentErrors.InvalidStudentsIds);

        foreach (var student in students)
            student.AdvisorId = null;

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}