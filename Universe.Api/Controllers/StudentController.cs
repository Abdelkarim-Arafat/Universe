using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universe.Api.Extensions;
using Universe.Application.Common;
using Universe.Application.UserServices.Commands.ChangePassword;
using Universe.Application.UserServices.Commands.RegisterStudent;
using Universe.Application.UserServices.Commands.RemoveStudent;
using Universe.Application.UserServices.Commands.UpdateContactData;
using Universe.Application.UserServices.Commands.UpdateFamilyData;
using Universe.Application.UserServices.Commands.UpdateMilitaryData;
using Universe.Application.UserServices.Commands.UpdatePersonalData;
using Universe.Application.UserServices.Commands.UpdatePreviousQualification;
using Universe.Application.UserServices.Querys.GetAllStudents;
using Universe.Application.UserServices.Querys.GetContactData;
using Universe.Application.UserServices.Querys.GetMilitaryData;
using Universe.Application.UserServices.Querys.GetParentData;
using Universe.Application.UserServices.Querys.GetPersonalData;
using Universe.Application.UserServices.Querys.GetPreviousQualificationData;
using Universe.Application.UserServices.Querys.GetStudentAcademicHistory;
using Universe.Application.UserServices.Querys.GetStudentExams;
using Universe.Application.UserServices.Querys.GetStudentSchedule;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/students")]
[ApiController , Authorize]
public class StudentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    public Guid GetUserId() => Guid.Parse(User.GetUserId()!);

    [HttpDelete("{studentId:guid}")]
    public async Task<IActionResult> RemoveStudent(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveStudentCommand(studentId), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }


    [HttpPost("")]
    public async Task<IActionResult> RegisterStudent (
        [FromRoute] Guid collegeId,
        [FromBody] RegisterStudentCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };
        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllStudent(
        [FromRoute] Guid collegeId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllStudentsCommand(collegeId, filter), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("contact-data")]
    public async Task<IActionResult> GetContactData (
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if(User.IsInRole("Student")) studentId = GetUserId();

        var result = await _mediator.Send(new GetContactDataCommand(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("parent-data")]
    public async Task<IActionResult> GetFamilyData(
       [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        var result = await _mediator.Send(new GetParentDataCommand(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("military-data")]
    public async Task<IActionResult> GetMilitaryData(
       [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        var result = await _mediator.Send(new GetMilitaryDataCommand(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("personal-data")]
    public async Task<IActionResult> GetPersonalData(
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();
        var result = await _mediator.Send(new GetPersonalDataCommand(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("previous-qualification-data")]
    public async Task<IActionResult> GetPreviousQualification(
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();
        var result = await _mediator.Send(new GetPreviousQualificationDataCommand(studentId!.Value), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }


    [HttpPut("contact-data")]
    public async Task<IActionResult> UpdateContactData(
        [FromBody] UpdateContactDataCommand request,
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        request = request with { StudentId = studentId!.Value };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("parent-data")]
    public async Task<IActionResult> UpdateFamilyData(
        [FromBody] UpdateParentDataCommand request,
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        request = request with { StudentId = studentId!.Value };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("military-data")]
    public async Task<IActionResult> UpdateMilitaryData(
        [FromBody] UpdateMilitaryDataCommand request,
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        request = request with { StudentId = studentId!.Value };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("personal-data")]
    public async Task<IActionResult> UpdatePersonalData(
        [FromRoute] Guid collegeId,
        [FromBody] UpdatePersonalDataCommand request,
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        request = request with { StudentId = studentId!.Value, CollegeId = collegeId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("previous-qualification-data")]
    public async Task<IActionResult> UpdatePreviousQualification(
        [FromBody] UpdatePreviousQualificationCommand request,
        [FromQuery] Guid? studentId,
        CancellationToken cancellationToken)
    {
        if (User.IsInRole("Student")) studentId = GetUserId();

        request = request with { StudentId = studentId!.Value };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("academic-history")]
    public async Task<IActionResult> GetStudentAcademicHistory(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentAcademicHistoryCommand(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("student-schedule")]
    public async Task<IActionResult> GetStudentSchedule(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentScheduleQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("student-exams")]
    public async Task<IActionResult> GetStudentExams(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentExamsQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}