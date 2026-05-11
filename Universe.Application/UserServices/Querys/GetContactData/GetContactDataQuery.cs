using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Querys.GetContactData;

public record GetContactDataQuery (
    [Required] Guid StudentId
) : IRequest<Result<ContactDataResponse>>;