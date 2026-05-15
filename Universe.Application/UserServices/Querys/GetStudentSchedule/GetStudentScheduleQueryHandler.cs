using Universe.Core.Contracts.Enrollments;

namespace Universe.Application.UserServices.Querys.GetStudentSchedule;

public class GetStudentScheduleQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetStudentScheduleQuery, Result<List<StudentExistingEnrollment>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<StudentExistingEnrollment>>> Handle(GetStudentScheduleQuery request, CancellationToken cancellationToken)
    {
        var isUserExist = await _unitOfWork.UserRepository.IsUserExistAsync(request.StudentId, cancellationToken);

        if (!isUserExist)
            return Result.Failure<List<StudentExistingEnrollment>>(StudentErrors.UserNotFound);

        var studentCollegeId = await _unitOfWork.UserRepository.GetStudentCollegeIdAsync(request.StudentId, cancellationToken);

        if (!studentCollegeId.HasValue)
            return Result.Failure<List<StudentExistingEnrollment>>(CollegeErrors.NotFound);

        var currentYear = await _unitOfWork.AcademicYearRepository
            .GetCurrentYearAsync(studentCollegeId.Value, cancellationToken);

        if (currentYear == null)
            return Result.Failure<List<StudentExistingEnrollment>>(AcademicYearErrors.NotFound);

        var currentSemester = await _unitOfWork.AcademicYearRepository
            .GetCurrentSemesterAsync(currentYear.Id, cancellationToken);

        if (currentSemester == null)
            return Result.Failure<List<StudentExistingEnrollment>>(SemesterErrors.NotFound);

        var studentSchedule = await _unitOfWork.EnrollmentRepository
            .GetStudentScheduleAsync(request.StudentId, currentSemester.Id, cancellationToken);

        return Result.Success(studentSchedule);
    }
}
