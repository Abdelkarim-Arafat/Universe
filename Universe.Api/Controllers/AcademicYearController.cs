using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Universe.Api.Extensions;
using Universe.Application.AcademicYearAndSemestersServices.Commands.StartAcademicYear;
using Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateAcademicYear;
using Universe.Application.AcadimicYearAndSemestersServices.Commands.UpdateCurrentSemester;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYear;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYears;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentSemester;
using Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentYear;
using Universe.Application.Common;
using Universe.Core.Constants;

namespace Universe.Api.Controllers;

[Route("colleges/{collegeId:guid}/academic-years")]
[ApiController]

public class AcademicYearsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpGet("current")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetCurrentYear(
        [FromRoute] Guid collegeId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCurrentYearQuery(collegeId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{id:guid}")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetAcademicYear(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAcademicYearQuery(id), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{academicYearId:guid}/current-semester")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetCurrentSemester(
        [FromRoute] Guid academicYearId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCurrentSemesterQuery(academicYearId), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }
    [HttpGet("")]
    [EnableRateLimiting("ReadLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisorOrStaff)]
    public async Task<IActionResult> GetAllYears(
        [FromRoute] Guid collegeId,
        [FromQuery] FilterRequest filter,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAcademicYearsQuery(collegeId , filter), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPost("")]
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
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
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
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
    [EnableRateLimiting("WriteLimiter")]
    [Authorize(Roles = Roles.AdminOrAdvisor)]
    public async Task<IActionResult> UpdateCurrentSemester(
        [FromRoute] Guid academicYearId,
        [FromBody] UpdateCurrentSemesterCommand request,
        CancellationToken cancellationToken)
    {
        request = request with { AcademicYearId = academicYearId };

        var result = await _mediator.Send(request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}