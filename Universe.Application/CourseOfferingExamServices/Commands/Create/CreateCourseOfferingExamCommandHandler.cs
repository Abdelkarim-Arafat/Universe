
namespace Universe.Application.CourseOfferingExamServices.Commands.Create;

public class CreateCourseOfferingExamCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCourseOfferingExamCommand, Result<CourseOfferingExamResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingExamResponse>> Handle(CreateCourseOfferingExamCommand request, CancellationToken cancellationToken)
    {
        var isExamAlreadyExist = await _unitOfWork.ExamRepository
        .IsCourseOfferingExamExistAsync(request.CourseOfferingId, request.ExamTermId, cancellationToken);

        if (isExamAlreadyExist)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.CourseOfferingExamIsExist);

        var isCourseOfferingExist = await _unitOfWork.CourseOfferingRepository
            .IsExistAsync(request.CourseOfferingId, cancellationToken);

        if (!isCourseOfferingExist)
            return Result.Failure<CourseOfferingExamResponse>(CourseOfferingErrors.NotFound);

        var isExamTermExist = await _unitOfWork.ExamRepository
            .IsExistExamTermAsync(request.ExamTermId, cancellationToken);

        if (!isExamTermExist)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.ExamTermNotFound);

        var committeesDetails = await _unitOfWork.ExamRepository
            .GetCommitteesDetailsAsync(request.ExamTermId, request.ExamCommitteesIds, cancellationToken);

        if (committeesDetails == null || !committeesDetails.Any())
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.ExamCommitteeNotFound);

        var hasOverlappingExam = await _unitOfWork.ExamRepository
            .HasOverlappingExamAsync
            (null, request.ExamTermId, request.ExamCommitteesIds, request.Date, request.StartTime, request.EndTime, cancellationToken);

        if (hasOverlappingExam)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.OverlappingTime);

        var studentsIds = await _unitOfWork.CourseOfferingRepository
            .GetStudentsIdsEnrolledInCourseAsync(request.CourseOfferingId, cancellationToken);


        var committeesCapacitiesSum = committeesDetails.Sum(c => c.Capacity);
        var numberOfRegistredStudents = studentsIds.Count();

        if (committeesCapacitiesSum < numberOfRegistredStudents)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.TotalCapacitiesIsNotEnough);

        var courseOfferingExam = request.Adapt<CourseOfferingExam>();

        var examSeatsToAdd = new List<ExamSeat>();
        var courseOfferingCommitteesToAdd = new List<CourseOfferingCommittee>();

        int seatNumber = 1;

        foreach (var committee in committeesDetails)
        {
            if (seatNumber > numberOfRegistredStudents)
                break;

            var courseOfferingCommittee = new CourseOfferingCommittee
            {
                CourseOfferingExamId = courseOfferingExam.Id,
                ExamCommitteeId = committee.Id
            };

            courseOfferingCommitteesToAdd.Add(courseOfferingCommittee);

            for (int index = 0; (index < committee.Capacity) && (seatNumber <= numberOfRegistredStudents); index++, seatNumber++)
            {
                var studentIndex = seatNumber - 1;

                var examSeat = new ExamSeat
                {
                    CourseOfferingCommitteeId = courseOfferingCommittee.Id,
                    StudentId = studentsIds[studentIndex],
                    SeatNumber = seatNumber
                };

                examSeatsToAdd.Add(examSeat);
            }
        }

        using var trx = await _unitOfWork
          .BeginTransactionIsolatedAsync(cancellationToken);

        try
        {
            await _unitOfWork.Repository<CourseOfferingExam>()
                .AddAsync(courseOfferingExam, cancellationToken);

            await _unitOfWork.Repository<CourseOfferingCommittee>()
                .AddRangeAsync(courseOfferingCommitteesToAdd, cancellationToken);

            await _unitOfWork.Repository<ExamSeat>()
                .AddRangeAsync(examSeatsToAdd, cancellationToken);

            await _unitOfWork.CompleteAsync(cancellationToken);
            await trx.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await trx.RollbackAsync(cancellationToken);

            return Result.Failure<CourseOfferingExamResponse>(
                new Error("500", ex.InnerException?.Message ?? ex.Message, StatusCodes.Status409Conflict));
        }

        var response = new CourseOfferingExamResponse
            (
            courseOfferingExam.Id,
            courseOfferingExam.Date,
            courseOfferingExam.StartTime,
            courseOfferingExam.EndTime
            );

        return Result.Success(response);
    }
}