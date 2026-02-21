
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Commands.UpdateFamilyData;

public record UpdateFamilyDataCommand(
    [Required] Guid UserId,
    string GuardianName,
    string RelationshipDegree,
    string Job,
    string MotherName,
    string City,
    string Email,
    string PhoneNumber,
    string Address
) : IRequest<Result<FamilyDataResponse>>;