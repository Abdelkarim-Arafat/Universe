using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface IStudentSemesterSummaryRepository 
{
    Task<List<StudentSemesterSummary>> GetStudentSummariesAsync(Guid studentId,CancellationToken cancellationToken);
}
