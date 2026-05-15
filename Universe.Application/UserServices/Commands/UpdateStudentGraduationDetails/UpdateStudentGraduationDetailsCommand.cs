using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Commands.UpdateStudentGraduationDetails;

public record UpdateStudentGraduationDetailsCommand (
    [Required] Guid Id,
    string GraduationYear,
    string GraduationSemester,
    string GraduationProjectName
) : IRequest<Result>;
