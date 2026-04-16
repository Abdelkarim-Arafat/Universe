using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Universe.Api.Extensions;
using Universe.Application.AcademicYearAndSemestersServices.Commands.StartAcademicYear;
using Universe.Application.AcadimicYearAndSemestersServices.Commands.AnnounceResult;
using Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateAcademicYear;
using Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateCurrentSemester;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYear;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAllYears;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentSemester;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentYear;
using Universe.Application.Common;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/academic-years")]
[ApiController]
public class AcademicYearsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentYear([FromRoute] Guid collegeId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCurrentYearCommand(collegeId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAcademicYear([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAcademicYearCommand(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{academicYearId:guid}/current-semester")]
    public async Task<IActionResult> GetCurrentSemester([FromRoute] Guid academicYearId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCurrentSemesterCommand(academicYearId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAllYears(
        [FromRoute] Guid collegeId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllYearsCommand(collegeId , filter), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> StartAcademicYear(
        [FromRoute] Guid collegeId,
        [FromBody] StartAcademicYearCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAcademicYear(
        [FromRoute] Guid collegeId,
        [FromRoute] Guid id,
        [FromBody] UpdateAcademicYearCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { CollegeId = collegeId, Id = id };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPatch("{academicYearId:guid}/current-semester")]
    public async Task<IActionResult> UpdateCurrentSemester(
        [FromRoute] Guid academicYearId,
        [FromBody] UpdateCurrentSemesterCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { AcademicYearId = academicYearId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
   
}