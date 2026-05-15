
namespace Universe.Application.EnrollmentServices.Queries.GetEnrollmentPage;

public class GetEnrollmentPageQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEnrollmentPageQuery, Result<EnrollmentPageResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<EnrollmentPageResponse>> Handle(GetEnrollmentPageQuery query, CancellationToken cancellationToken)
    {
        var isUserExist = await _unitOfWork.UserRepository.IsUserExistAsync(query.StudentId, cancellationToken);

        if (!isUserExist)
            return Result.Failure<EnrollmentPageResponse>(StudentErrors.NotFound);

        var isSemesterExist = await _unitOfWork.AcademicYearRepository
            .IsSemesterExistAsync(query.SemesterId, cancellationToken);

        if (!isSemesterExist)
            return Result.Failure<EnrollmentPageResponse>(SemesterErrors.NotFound);

        var isLevelExist = await _unitOfWork.LevelRepository
            .IsLevelExistAsync(query.LevelId, cancellationToken);

        if (!isLevelExist)
            return Result.Failure<EnrollmentPageResponse>(LevelErrors.NotFound);

        var studentCurrentProgramId = await _unitOfWork.AcademicProgramRepository
           .GetStudentCurrentProgramIdAsync(query.StudentId, cancellationToken);

        if (!studentCurrentProgramId.HasValue)
            return Result.Failure<EnrollmentPageResponse>(StudentErrors.NoProgram);

        var totalEarnedHours = await _unitOfWork.UserRepository
           .CalculateCreditHoursAsync(query.StudentId, null, cancellationToken);

        var studentCurrentLevelId = await _unitOfWork.LevelRepository
            .GetStudentCurrentLevelIdAsync
            (studentCurrentProgramId.Value, totalEarnedHours, cancellationToken);

        if (!studentCurrentLevelId.HasValue)
            return Result.Failure<EnrollmentPageResponse>(LevelErrors.StudentLevelNotFound);

        var studentLevelStudyLoad = await _unitOfWork.StudyLoadByLevelRepository
            .GetLevelStudyLoadAsync(studentCurrentLevelId.Value, query.SemesterId, cancellationToken);

        if (studentLevelStudyLoad == null)
            return Result.Failure<EnrollmentPageResponse>(StudyLoadByLevelErrors.NotFound);

        var existingEnrollmentsInfos = await _unitOfWork.EnrollmentRepository
            .GetExistingEnrollmentsInfoAsync(query.StudentId, query.SemesterId, cancellationToken);
         
        var availableCourses = await _unitOfWork.CourseOfferingRepository
               .GetAvailableCoursesForRegistrationAsync
               (query.StudentId, query.SemesterId, query.LevelId, cancellationToken);

        decimal Gpa = await _unitOfWork.UserRepository
            .CalculateGpaAsync(query.StudentId, null, studentCurrentProgramId.Value, cancellationToken);

        var studentData = await _unitOfWork.Repository<Student>()
            .GetQueryable()
            .Where(s => s.Id == query.StudentId && !s.IsDeleted)
            .Select(s => new
            {
                s.Name,
                s.StudentCode
            })
            .FirstOrDefaultAsync(cancellationToken);

        var currentRegisteredHours = await _unitOfWork.EnrollmentRepository
            .CalculateCurrentRegisteredHoursAsync(query.StudentId, query.SemesterId, cancellationToken);

        var StudentInfo = new StudentInfoResponse
           (
            studentData!.Name,
            studentLevelStudyLoad.LevelName,
            studentData.StudentCode,
            currentRegisteredHours,
            studentLevelStudyLoad.MaxHours,
            studentLevelStudyLoad.MinHours,
            Gpa
        );

        var Response = new EnrollmentPageResponse(
            StudentInfo,
            availableCourses,
            existingEnrollmentsInfos);

        return Result.Success(Response);
    }
}
