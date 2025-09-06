using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Statistics.Queries.GetNumberOfProcedures;
using Template.Application.Statistics.Queries.GetNumberOfUsersInEachRole;
using Template.Application.Statistics.Queries.GetTopFiveAssistantsByAssignments;
using Template.Application.Statistics.Queries.GetTopFiveAssistantsByRatings;
using Template.Application.Statistics.Queries.GetTopFiveDoctors;
using Template.Application.Users.Dtos;
using Template.Domain.Enums;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetNumberOfUsersInEachRole")]
    [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
    public async Task<ActionResult<Dictionary<string, int>>> GetNumberOfUsersInEachRole()
    {
        var result = await mediator.Send(new GetNumberOfUsersInEachRoleQuery());
        return Ok(result);
    }

    [HttpGet("GetNumberOfProcedures")]
    [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
    public async Task<ActionResult<int>> GetNumberOfProcedures([FromQuery] GetNumberOfProceduresQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(new { Result = result });
    }

    [HttpGet("GetTopFiveAssistantsByRatings")]
    [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetTopFiveAssistantsByRatings()
    {
        var result = await mediator.Send(new GetTopFiveAssistantsByRatingsQuery());
        return Ok(result);
    }

    [HttpGet("GetTopFiveAssistantsByAssignments")]
    [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
    public async Task<ActionResult<IEnumerable<UserProcedureCountDto>>> GetTopFiveAssistantsByAssignments()
    {
        var result = await mediator.Send(new GetTopFiveAssistantsByAssignmentsQuery());
        return Ok(result);
    }

    [HttpGet("GetTopFiveDoctors")]
    [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
    public async Task<ActionResult<IEnumerable<UserProcedureCountDto>>> GetTopFiveDoctors()
    {
        var result = await mediator.Send(new GetTopFiveDoctorsQuery());
        return Ok(result);
    }
}
