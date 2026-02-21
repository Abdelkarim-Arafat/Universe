
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Commands.UpdateContactData;

public record UpdateContactDataCommand(
    Guid UserId,
    string City,
    string Email,
    string PhoneNumber,
    string Address,
    string PostalCode
) : IRequest<Result<ContactDataResponse>>;