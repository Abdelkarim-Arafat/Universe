using Universe.Application.ExamServices.CourseOfferingExamServices.Dtos;

namespace Universe.Application.ExamServices.CourseOfferingExamServices.Commands.UpsertCourseExamCommittees;

public class UpsertCourseExamCommitteesCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpsertCourseExamCommitteesCommand, Result<PaginationList<CourseExamCommitteesResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PaginationList<CourseExamCommitteesResponse>>> Handle(UpsertCourseExamCommitteesCommand command, CancellationToken cancellationToken)
    {
        var courseOfferingExam = await _unitOfWork.ExamRepository
          .GetCourseOfferingExamByIdAsync(command.CourseOfferingExamId, cancellationToken);

        if (courseOfferingExam == null)
            return Result.Failure<PaginationList<CourseExamCommitteesResponse>>(ExamErrors.CourseOfferingExamNotFound);

        var incomingCommitteesIds = command.ExamCommitteesIds;

        var checkOverLappedInCommittees = await _unitOfWork.ExamRepository
            .CheckOverLappedInCommitteesAsync(courseOfferingExam, incomingCommitteesIds, cancellationToken);

        if (checkOverLappedInCommittees)
            return Result.Failure<PaginationList<CourseExamCommitteesResponse>>
                (ExamErrors.OverlappingTimeInCommittees);

        var existingCommitteesIds = await _unitOfWork.ExamRepository
            .GetCourseOfferingsCommitteesIdsAsync(command.CourseOfferingExamId, cancellationToken);

        var committeesCapacitiesSum = await _unitOfWork.ExamRepository
            .CommitteesCapacitiesSumAsync(incomingCommitteesIds, cancellationToken);

        var studentsIds = await _unitOfWork.EnrollmentRepository
            .GetStudentsIdsByCourseOfferingAsync(courseOfferingExam.CourseOfferingId, cancellationToken);

        var numberOfRegistredStudents = studentsIds.Count();

        if (committeesCapacitiesSum < numberOfRegistredStudents)
            return Result.Failure<PaginationList<CourseExamCommitteesResponse>>(ExamErrors.TotalCapacitiesIsNotEnough);


      
            var examSeatsToDelete = new List<ExamSeat>();
            var courseOfferingCommitteesToDelete = new List<CourseOfferingCommittee>();

            var examSeatsToAdd = new List<ExamSeat>();
            var courseOfferingCommitteesToAdd = new List<CourseOfferingCommittee>();

            // remove old committees 
            var courseOfferingCommittees = await _unitOfWork.ExamRepository
                   .GetCourseOfferingCommitteesIncludingSeatsAsync(existingCommitteesIds, cancellationToken);

            foreach (var courseOfferingCommit in courseOfferingCommittees)
            {
                foreach (var seat in courseOfferingCommit.ExamSeats)
                    examSeatsToDelete.Add(seat);

                courseOfferingCommitteesToDelete.Add(courseOfferingCommit);
            }

            var committeeToCapacity = await _unitOfWork.ExamRepository
                .GetCommitteeCapacitiesLookupAsync(incomingCommitteesIds, cancellationToken);

            int seatNumber = 1;

            foreach (var committeeId in incomingCommitteesIds)
            {
                if (seatNumber > numberOfRegistredStudents)
                    break;

                if (!committeeToCapacity.TryGetValue(committeeId, out var committeeCapacity))
                    return Result.Failure<PaginationList<CourseExamCommitteesResponse>>(ExamErrors.ExamCommitteeNotFound);

                var courseOfferingCommittee = new CourseOfferingCommittee
                {
                    CourseOfferingExamId = command.CourseOfferingExamId,
                    ExamCommitteeId = committeeId
                };

                courseOfferingCommitteesToAdd.Add(courseOfferingCommittee);

                for (int index = 0; (index < committeeCapacity) && (seatNumber <= numberOfRegistredStudents); index++, seatNumber++)
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
            if (courseOfferingCommitteesToDelete.Any())
            {
                _unitOfWork.Repository<CourseOfferingCommittee>().DeletePermanentlyRange(courseOfferingCommitteesToDelete);
                _unitOfWork.Repository<ExamSeat>().DeletePermanentlyRange(examSeatsToDelete);
            }

            await _unitOfWork.Repository<CourseOfferingCommittee>()
                .AddRangeAsync(courseOfferingCommitteesToAdd, cancellationToken);
            await _unitOfWork.Repository<ExamSeat>().AddRangeAsync(examSeatsToAdd, cancellationToken);

            await _unitOfWork.CompleteAsync(cancellationToken);
            await trx.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await trx.RollbackAsync(cancellationToken);

            return Result.Failure<PaginationList<CourseExamCommitteesResponse>>(
                new Error("500", ex.InnerException?.Message ?? ex.Message, StatusCodes.Status409Conflict));
        }


        var query = _unitOfWork.Repository<CourseOfferingCommittee>()
                   .GetQueryable()
                   .Where(com => !com.IsDeleted && com.CourseOfferingExamId == command.CourseOfferingExamId);

        var filter = command.Filter;

        var source = query.Select(com => new CourseExamCommitteesResponse
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
            source = source.Where(x =>
                x.Place.Contains(filter.SearchValue) ||
                x.CommitteeNumber.ToString().Contains(filter.SearchValue));
        

        if (!string.IsNullOrEmpty(filter.SortColumn))
            source = source.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var response = await PaginationList<CourseExamCommitteesResponse>
             .CreateAsync(source, filter.PageNumber, filter.PageSize, cancellationToken);

        return Result.Success(response);
    }
}
