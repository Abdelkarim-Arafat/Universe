namespace Universe.Application.StudentServices.Queries.GetStudentExams;

public class GetStudentExamsQueryHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetStudentExamsQuery, Result<StudentExamsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<StudentExamsResponse>> Handle(GetStudentExamsQuery request, CancellationToken cancellationToken)
    {
        var StudentId = request.StudentId;

        var IsStudentExist = await _unitOfWork.UserRepository.IsUserExistAsync(StudentId, cancellationToken);

        if (!IsStudentExist)
            return Result.Failure<StudentExamsResponse>(StudentErrors.UserNotFound);

        var studentCurrentProgramId = await _unitOfWork.AcademicProgramRepository
            .GetStudentCurrentProgramIdAsync(StudentId, cancellationToken);

        if (studentCurrentProgramId == null)
            return Result.Failure<StudentExamsResponse>(AcademicProgramErrors.NotFound);

        var studentCollegeId = await _unitOfWork.UserRepository
            .GetStudentCollegeIdAsync(StudentId, cancellationToken);

        if (!studentCollegeId.HasValue)
            return Result.Failure<StudentExamsResponse>(CollegeErrors.NotFound);

        var currentYear = await _unitOfWork.AcademicYearRepository
            .GetCurrentYearAsync(studentCollegeId.Value, cancellationToken);

        if (currentYear == null)
            return Result.Failure<StudentExamsResponse>(AcademicYearErrors.NotFound);

        var currentSemester = await _unitOfWork.AcademicYearRepository
            .GetCurrentSemesterAsync(currentYear.Id, cancellationToken);

        if (currentSemester == null)
            return Result.Failure<StudentExamsResponse>(SemesterErrors.NotFound);

        var currentCoursesIds = await _unitOfWork.EnrollmentRepository
            .GetRegisteredCourseOfferingIdsInCurrentSemesterAsync(StudentId, currentSemester.Id, cancellationToken);

        var examTermsIds = await _unitOfWork.ExamRepository
            .GetCurrentExamTermIdsAsync(studentCurrentProgramId.Value, currentSemester.Id, cancellationToken);

        var studentExams = await _unitOfWork.UserRepository
            .GetStudentExamsTablesAsync(StudentId, currentCoursesIds, examTermsIds, cancellationToken);

        var studentInfo = _unitOfWork.Repository<Student>()
            .GetQueryable()
            .Where(student => !student.IsDeleted && student.Id == StudentId)
            .Select(student => new
            {
                student.Name,
                Code = student.StudentCode
            }).FirstOrDefault();

        if (studentInfo == null)
            return Result.Failure<StudentExamsResponse>(StudentErrors.UserNotFound);

        return Result.Success(new StudentExamsResponse(studentInfo.Name, studentInfo.Code, studentExams));
    }
}
