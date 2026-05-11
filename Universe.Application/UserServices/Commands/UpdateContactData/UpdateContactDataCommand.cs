
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Commands.UpdateContactData;

public record UpdateContactDataCommand(
    Guid StudentId,
    string City,
    string Email,
    string PhoneNumber,
    string Address,
    string PostalCode
) : IRequest<Result<ContactDataResponse>>;