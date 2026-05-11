
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Commands.UpdateFamilyData;

public record UpdateParentDataCommand(
    [Required] Guid StudentId,
    string GuardianName,
    string RelationshipDegree,
    string Job,
    string MotherName,
    string GuardianCity,
    string GuardianEmail,
    string GuardianPhoneNumber,
    string GuardianAddress
) : IRequest<Result<ParentDataResponse>>;