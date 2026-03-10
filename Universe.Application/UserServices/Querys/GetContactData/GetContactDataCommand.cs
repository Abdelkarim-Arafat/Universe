using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Querys.GetContactData;

public record GetContactDataCommand (
    [Required] Guid StudentId
) : IRequest<Result<ContactDataResponse>>;