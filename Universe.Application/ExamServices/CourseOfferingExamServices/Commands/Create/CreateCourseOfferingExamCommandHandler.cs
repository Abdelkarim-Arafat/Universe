using Universe.Application.CourseOfferingExamServices.Dtos;

namespace Universe.Application.CourseOfferingExamServices.Commands.Create;

public class CreateCourseOfferingExamCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCourseOfferingExamCommand, Result<CourseOfferingExamResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingExamResponse>> Handle(CreateCourseOfferingExamCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _unitOfWork.ExamRepository
            .CreateCourseExamValidationAsync
            (request.Date, request.StartTime, request.EndTime,
            request.CourseOfferingId, request.ExamTermId, request.ExamCommitteesIds,
            cancellationToken);

        if (validationResult.isCourseOfferingExamExist)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.CourseOfferingExamIsExist);

        var existingIds = validationResult.examCommittees.Select(ec => ec.Id);

        if (request.ExamCommitteesIds.Except(existingIds).Any())
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.ExamCommitteeNotFound);

        if (!validationResult.isCourseOfferingExist)
            return Result.Failure<CourseOfferingExamResponse>(CourseOfferingErrors.NotFound);

        if (!validationResult.isExamTermExist)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.ExamTermNotFound);

        if (validationResult.isOverlappedTime)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.OverlappingTime);

        var studentsIds = validationResult.studentsIds.ToList();

        var committeessSum = validationResult.examCommittees.Sum(c => c.Capacity);
        var studentsCount = studentsIds.Count();

        if (committeessSum < studentsCount)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.TotalCapacitiesIsNotEnough);

        var courseOfferingExam = request.Adapt<CourseOfferingExam>();

        var examSeatsToAdd = new List<ExamSeat>();
        var courseOfferingCommitteesToAdd = new List<CourseOfferingCommittee>();

        int seatNumber = 1;

        foreach (var committee in validationResult.examCommittees)
        {
            if (seatNumber > studentsCount)
                break;

            var courseOfferingCommittee = new CourseOfferingCommittee
            {
                CourseOfferingExamId = courseOfferingExam.Id,
                ExamCommitteeId = committee.Id
            };

            courseOfferingCommitteesToAdd.Add(courseOfferingCommittee);

            for (int index = 0; (index < committee.Capacity) && (seatNumber <= studentsCount); index++, seatNumber++)
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
          .Repository<CourseOfferingCommittee>()
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

        var query = _unitOfWork.Repository<CourseOfferingCommittee>()
                 .GetQueryable()
                 .Where(com => !com.IsDeleted && com.CourseOfferingExamId == courseOfferingExam.Id);

        var filter = request.Filter;

        var source = query.Select(com => new CourseExamCommittees
               (
                com.ExamCommitteeId,
                com.ExamCommittee.CommitteeNumber,
                com.ExamCommittee.MaxCapacity,
                com.ExamSeats.Count(seat => !seat.IsDeleted),
                com.ExamSeats.Where(seat => !seat.IsDeleted).Select(seat => (int?)seat.SeatNumber).Min() ?? 0, // check if null
                com.ExamCommittee.Room != null
                ? $"{com.ExamCommittee.Room.RoomNumber} - {com.ExamCommittee.Room.Building.Name}"
                : "No Place"
             ));

        if (!string.IsNullOrEmpty(filter.SearchValue))
            source = source.ApplySearch(filter.SearchValue, x => x.Place);

        if (!string.IsNullOrEmpty(filter.SortColumn))
            source = source.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var committeesResponse = await PaginationList<CourseExamCommittees>
             .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        var response = new CourseOfferingExamResponse
            (
            courseOfferingExam.Id,
            courseOfferingExam.Date,
            courseOfferingExam.StartTime,
            courseOfferingExam.EndTime,
            committeesResponse
            );

        return Result.Success(response);
    }
}