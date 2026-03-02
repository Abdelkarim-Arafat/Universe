using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicProgramServices.Commands.RemoveAcademicProgram;

public record RemoveAcademicProgramCommand(
    [Required]Guid Id
) : IRequest<Result>;
