using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.EnrollmentServices.Dtos;

namespace Universe.Application.UserServices.Querys.GetStudentSchedule;

public record GetStudentScheduleQuery
() : IRequest<Result<List<EnrollmentInfo>>>;
