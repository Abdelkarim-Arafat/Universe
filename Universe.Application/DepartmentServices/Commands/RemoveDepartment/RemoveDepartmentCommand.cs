using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.DepartmentServices.Commands.RemoveDepartment;

public record RemoveDepartmentCommand(
    [Required]Guid Id
) : IRequest<Result>;
