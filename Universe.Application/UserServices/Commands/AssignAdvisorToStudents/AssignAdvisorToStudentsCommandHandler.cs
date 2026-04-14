using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Universe.Application.UserServices.Commands.AssignAdvisorToStudents;

public class AssignAdvisorToStudentsCommandHandler(
    IUnitOfWork unitOfWork,
    UserManager<ApplicationUser> userManager
) : IRequestHandler<AssignAdvisorToStudentsCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> Handle(
        AssignAdvisorToStudentsCommand request,
        CancellationToken cancellationToken)
    {
        if(!await _userManager.Users
            .AnyAsync(u => u.Id == request.AdvisorId && !u.IsDeleted, cancellationToken)
            ) return Result.Failure(AuthErrors.UserNotFound);

        var students = await _unitOfWork.UserRepository
            .GetStudentsByIdsAsync(request.StudentIds, cancellationToken);

        if(students.Count != request.StudentIds.Count) 
            return Result.Failure(StudentErrors.InvalidStudentsIds);

        foreach (var student in students) 
            student.AdvisorId = request.AdvisorId;

        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success();
    }
}