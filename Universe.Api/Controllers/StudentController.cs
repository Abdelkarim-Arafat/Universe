using MediatR;
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

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/students")]
[ApiController]
public class StudentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete("{studentId:guid}")]
    public async Task<IActionResult> RemoveStudent(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveStudentCommand(studentId), cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }


    [HttpPost("")]
    public async Task<IActionResult> RegisterStudent(
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

    [HttpGet("{studentId:guid}/contact-data")]
    public async Task<IActionResult> GetContactData(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetContactDataCommand(studentId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{studentId:guid}/parent-data")]
    public async Task<IActionResult> GetFamilyData(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetParentDataCommand(studentId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{studentId:guid}/military-data")]
    public async Task<IActionResult> GetMilitaryData(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(new GetMilitaryDataCommand(studentId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{studentId:guid}/personal-data")]
    public async Task<IActionResult> GetPersonalData(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(new GetPersonalDataCommand(studentId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{studentId:guid}/previous-qualification-data")]
    public async Task<IActionResult> GetPreviousQualification(
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(new GetPreviousQualificationDataCommand(studentId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }


    [HttpPut("{studentId:guid}/contact-data")]
    public async Task<IActionResult> UpdateContactData(
        [FromRoute] Guid studentId,
        [FromBody] UpdateContactDataCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("{studentId:guid}/parent-data")]
    public async Task<IActionResult> UpdateFamilyData(
        [FromRoute] Guid studentId,
        [FromBody] UpdateParentDataCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("{studentId:guid}/military-data")]
    public async Task<IActionResult> UpdateMilitaryData(
        [FromRoute] Guid studentId,
        [FromBody] UpdateMilitaryDataCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("{studentId:guid}/personal-data")]
    public async Task<IActionResult> UpdatePersonalData(
        [FromRoute] Guid studentId,
        [FromRoute] Guid collegeId,
        [FromBody] UpdatePersonalDataCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId, CollegeId = collegeId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("{studentId:guid}/previous-qualification-data")]
    public async Task<IActionResult> UpdatePreviousQualification(
        [FromRoute] Guid studentId,
        [FromBody] UpdatePreviousQualificationCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { StudentId = studentId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("academic-history")]
    public async Task<IActionResult> GetStudentAcademicHistory(Guid StudentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentAcademicHistoryCommand(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}