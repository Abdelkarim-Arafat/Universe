using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcadimicYearAndSemestersServices.Commands.AnnounceResult;

public record AnnounceResultCommand
([Required] Guid SemesterId) : IRequest<Result>;

