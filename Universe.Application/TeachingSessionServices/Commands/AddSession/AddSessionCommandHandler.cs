using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Commands.AddSession;

public class AddSessionCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<AddSessionCommand, Result<SessionResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<SessionResponse>> Handle(AddSessionCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.InstructorId && !x.IsDeleted) is not { } instructor
            ) return Result.Failure<SessionResponse>(AuthErrors.UserNotFound);

        if (await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(request.CourseOfferingId, cancellationToken) is not { } course
            ) return Result.Failure<SessionResponse> (CourseOfferingErrors.NotFound);

        if (course.NumberOfGroups < request.GroupNumber)
            return Result.Failure<SessionResponse>(CourseOfferingErrors.NotValidGroupNumber);

        if (await _unitOfWork.RoomRepository
            .GetByIdAsync(request.RoomId) is not { } room
            ) return Result.Failure<SessionResponse>(RoomErrors.RoomNotFound);

        if (room.Capacity < request.Capacity)
            return Result.Failure<SessionResponse>(RoomErrors.UnValidCapacity);

        if (await _unitOfWork.AcademicProgramRepository
            .GetScheduleAsync(course.AcademicProgramId, course.SemesterId, cancellationToken) is not { } schedule
            ) return Result.Failure<SessionResponse>(AcademicProgramErrors.ScheduleNotFound);

        bool notValid = request.StartTime < schedule.DayStartTime || request.EndTime > schedule.DayEndTime;
        notValid |= request.StartTime.Minute % schedule.SlotDurationMinutes != 0;
        notValid |= request.EndTime.Minute % schedule.SlotDurationMinutes != 0;

        if (notValid) return Result.Failure<SessionResponse>(SessionErrors.StartOrEndTimeAreNotValid);


        var session = await _unitOfWork.SessionRepository
                  .GetMatchingSessionAsync(
                      course.Id, request.StartTime, request.EndTime,
                      request.Day, request.Type, request.InstructorId,
                      request.RoomId, request.GroupNumber, cancellationToken);

        if (session is null)
        {
            if (await _unitOfWork.SessionRepository
                .IsUsedTimeAsync(course.Id, request.StartTime, request.EndTime, request.Day, request.GroupNumber, cancellationToken)
                ) return Result.Failure<SessionResponse>(SessionErrors.AlreadyUsedTime);

            if (await _unitOfWork.SessionRepository
                .IsInstructorBusyAsync(request.InstructorId, course.SemesterId, request.StartTime, request.EndTime, request.Day, cancellationToken)
                ) return Result.Failure<SessionResponse>(SessionErrors.InstructoreIsBusy);

            if (await _unitOfWork.SessionRepository
                .IsRoomBusyAsync(request.RoomId, course.SemesterId, request.StartTime, request.EndTime, request.Day, cancellationToken)
                ) return Result.Failure<SessionResponse>(SessionErrors.RoomIsBusy);

            session = request.Adapt<TeachingSession>();
            await _unitOfWork.Repository<TeachingSession>().AddAsync(session, cancellationToken);
        }

        var courseSession = new CourseOfferingSession
        {
            TeachingSessionId = session.Id,
            CourseOfferingId = course.Id
        };
        
        await _unitOfWork.Repository<CourseOfferingSession>().AddAsync(courseSession, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(SessionCacheKeys.Tags(course.Id), cancellationToken);

        var response = new SessionResponse(
            session.Id,
            session.StartTime,
            session.EndTime,
            session.Type,
            session.Day,
            instructor.Id,
            instructor.Name,
            room.Id,
            room.Name,
            session.GroupNumber
        );

        return Result.Success(response);
    }
}
