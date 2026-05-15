using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.UserServices.Commands.ChangePassword;
using Universe.Application.UserServices.Commands.ChangeStudentProgram;
using Universe.Application.UserServices.Commands.RegisterStudent;
using Universe.Application.UserServices.Commands.RemoveStudent;
using Universe.Application.UserServices.Commands.UpdateContactData;
using Universe.Application.UserServices.Commands.UpdateFamilyData;
using Universe.Application.UserServices.Commands.UpdateMilitaryData;
using Universe.Application.UserServices.Commands.UpdatePersonalData;
using Universe.Application.UserServices.Commands.UpdatePreviousQualification;
using Universe.Application.UserServices.Commands.UpdateStudentGraduationDetails;
using Universe.Application.UserServices.Querys.GetAllStudents;
using Universe.Application.UserServices.Querys.GetContactData;
using Universe.Application.UserServices.Querys.GetMilitaryData;
using Universe.Application.UserServices.Querys.GetParentData;
using Universe.Application.UserServices.Querys.GetPersonalData;
using Universe.Application.UserServices.Querys.GetPreviousQualificationData;
using Universe.Application.UserServices.Querys.GetStudentAcademicHistory;
using Universe.Application.UserServices.Querys.GetStudentExams;
using Universe.Application.UserServices.Querys.GetStudentGraduationDetails;
using Universe.Application.UserServices.Querys.GetStudentSchedule;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("students")]
[ApiController , Authorize]
public class StudentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    public Guid GetUserId() => Guid.Parse(User.GetUserId()!);


    [HttpPatch("{studentId:guid}/change-program")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> ChangeStudentProgram (
        [FromQuery] Guid newProgramId,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ChangeStudentProgramCommand(newProgramId, studentId), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPatch("{studentId:guid}/graduation-details")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdateGraduationDetails(
        [FromRoute] Guid studentId,
        [FromBody] UpdateStudentGraduationDetailsCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { Id = studentId };
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpDelete("{studentId:guid}")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> RemoveStudent (
        [FromQuery] Guid academicProgramId,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveStudentCommand(academicProgramId, studentId), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> RegisterStudent (
        [FromQuery] Guid collegeId,
        [FromQuery] Guid academicProgramId,
        [FromBody] RegisterStudentCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId , ProgramId = academicProgramId};
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetAllStudent (
        [FromQuery] Guid academicProgramId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProgramStudentsQuery(academicProgramId, filter), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("{studentId:guid}/graduation-details")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> GetStudentGraduationDetails(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentGraduationDetailsQuery(studentId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("contact-data")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> GetContactData (
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if(User.IsInRole("Student")) studentId = GetUserId();

        var result = await _mediator.Send(new GetContactDataQuery(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("parent-data")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> GetFamilyData(
       [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        var result = await _mediator.Send(new GetParentDataQuery(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("military-data")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> GetMilitaryData (
       [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        var result = await _mediator.Send(new GetMilitaryDataQuery(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("personal-data")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> GetPersonalData(
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();
        var result = await _mediator.Send(new GetPersonalDataQuery(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("previous-qualification-data")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> GetPreviousQualification(
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();
        var result = await _mediator.Send(new GetPreviousQualificationDataQuery(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }


    [HttpPut("contact-data")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdateContactData(
        [FromBody] UpdateContactDataCommand request,
        [FromQuery] Guid studentId,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("parent-data")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdateFamilyData(
        [FromBody] UpdateParentDataCommand request,
        [FromQuery] Guid studentId,
        CancellationToken cancellationToken)
    {

        request = request with { StudentId = studentId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("military-data")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdateMilitaryData (
        [FromBody] UpdateMilitaryDataCommand request,
        [FromQuery] Guid studentId,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("personal-data")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdatePersonalData(
        [FromQuery] Guid academicProgramId,
        [FromBody] UpdatePersonalDataCommand request,
        [FromQuery] Guid studentId,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId, ProgramId = academicProgramId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("previous-qualification-data")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdatePreviousQualification(
        [FromBody] UpdatePreviousQualificationCommand request,
        [FromQuery] Guid studentId,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("academic-history")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> GetStudentAcademicHistory(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentAcademicHistoryQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("student-schedule")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> GetStudentSchedule(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentScheduleQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("student-exams")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = $"{Roles.AdminOrAdvisor} , {Roles.Student}")]
    public async Task<IActionResult> GetStudentExams(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentExamsQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


}