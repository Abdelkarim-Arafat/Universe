using Universe.Application.UserServices.UserDtos;
using Universe.Core.Entities.StudentInfo;

namespace Universe.Application.UserServices.Commands.RegisterStudent;

public class RegisterStudentCommandHandler(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    ILogger<RegisterStudentCommandHandler> logger
    ) : IRequestHandler<RegisterStudentCommand, Result<RegisterStudentResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<RegisterStudentCommandHandler> _logger = logger;

    public async Task<Result<RegisterStudentResponse>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users.AnyAsync(x => x.CollegeId == request.CollegeId && x.UserName == request.UserName && !x.IsDeleted, cancellationToken))
            return Result.Failure<RegisterStudentResponse>(AuthErrors.DuplicateUserName);

        if(await _unitOfWork.UserRepository.IsStudentCodeExistsAsync(request.CollegeId , null , request.StudentCode , cancellationToken))
            return Result.Failure<RegisterStudentResponse>(StudentErrors.DuplicateStudentCode);

        if (await _unitOfWork.UserRepository.IsStudentNationalIdExistsAsync(request.CollegeId , null , request.NationalIdOrPassport, cancellationToken))
            return Result.Failure<RegisterStudentResponse>(StudentErrors.DuplicateNationalIdOrPassport);

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Name = request.Name,
            //Email = "karimm@gmail.com",
            CollegeId = request.CollegeId,
            Student = request.Adapt<Student>()
        };

        user.Student.ContactInfo = request.Adapt<ContactInfo>();
        user.Student.MilitaryInfo = request.Adapt<MilitaryInfo>();
        user.Student.ParentInfo = request.Adapt<ParentInfo>();
        user.Student.PreviousQualification = request.Adapt<PreviousQualification>();

        user.Student.Id = user.Id;
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<RegisterStudentResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        await _userManager.AddToRoleAsync(user, "Student");
        return Result.Success(new RegisterStudentResponse(user.Id.ToString(), user.Name, user.UserName));
    }
}

