using Universe.Application.CourseOfferingExamServices.Dtos;

namespace Universe.Application.CourseOfferingExamServices.Commands.Update;

public class UpdateCourseOfferingExamCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateCourseOfferingExamCommand, Result<CourseOfferingExamResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingExamResponse>> Handle(UpdateCourseOfferingExamCommand request, CancellationToken cancellationToken)
    {
        var courseExamContext = await _unitOfWork.ExamRepository
           .UpdateCourseExamContextAsync
           (request.Date, request.StartTime, request.EndTime,
           request.Id, request.ExamCommitteesIds, cancellationToken);

        if (courseExamContext == null)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.CourseOfferingExamNotFound);

        var courseOfferingExam = courseExamContext.CourseOfferingExam;

        courseOfferingExam.Adapt(request);


        var checkOverLappedInCommittees = courseExamContext.isOverlappedTime;

        if (checkOverLappedInCommittees)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.OverlappingTimeInCommittees);

        var committeesCapacitiesSum = courseExamContext.examCommittees.Sum(ec => ec.Capacity);

        var studentsIds = courseExamContext.studentsIds;

        var numberOfRegistredStudents = studentsIds.Count();

        if (committeesCapacitiesSum < numberOfRegistredStudents)
            return Result.Failure<CourseOfferingExamResponse>(ExamErrors.TotalCapacitiesIsNotEnough);

        var examSeatsToDelete = new List<ExamSeat>();
        var courseOfferingCommitteesToDelete = new List<CourseOfferingCommittee>();

        var examSeatsToAdd = new List<ExamSeat>();
        var courseOfferingCommitteesToAdd = new List<CourseOfferingCommittee>();

        var committeesToRemove = courseExamContext.committeesToRemove;

        foreach (var committee in committeesToRemove)
        {
            foreach (var seat in committee.ExamSeats)
                examSeatsToDelete.Add(seat);

            courseOfferingCommitteesToDelete.Add(committee);
        }

        int seatNumber = 1;

        foreach (var committee in courseExamContext.examCommittees)
        {
            if (seatNumber > numberOfRegistredStudents)
                break;

            var courseOfferingCommittee = new CourseOfferingCommittee
            {
                CourseOfferingExamId = courseOfferingExam.Id,
                ExamCommitteeId = committee.Id
            };

            courseOfferingCommitteesToAdd.Add(courseOfferingCommittee);

            for (int index = 0; (index < committee.Capacity) 
                                && (seatNumber <= numberOfRegistredStudents); index++, seatNumber++)
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
            _unitOfWork.Repository<ExamSeat>()
               .DeletePermanentlyRange(examSeatsToDelete);

            _unitOfWork.Repository<CourseOfferingCommittee>()
               .DeletePermanentlyRange(courseOfferingCommitteesToDelete);

            _unitOfWork.Repository<CourseOfferingExam>().Update(courseOfferingExam);

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