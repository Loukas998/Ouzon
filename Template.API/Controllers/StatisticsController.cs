using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Statistics.Queries.GetNumberOfProcedures;
using Template.Application.Statistics.Queries.GetNumberOfUsersInEachRole;
using Template.Application.Statistics.Queries.GetTopFiveAssistantsByAssignments;
using Template.Application.Statistics.Queries.GetTopFiveAssistantsByRatings;
using Template.Application.Statistics.Queries.GetTopFiveDoctors;
using Template.Application.Users.Dtos;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetNumberOfUsersInEachRole")]
    public async Task<ActionResult<Dictionary<string, int>>> GetNumberOfUsersInEachRole()
    {
        var result = await mediator.Send(new GetNumberOfUsersInEachRoleQuery());
        return Ok(result);
    }

    [HttpGet("GetNumberOfProcedures")]
    public async Task<ActionResult<int>> GetNumberOfProcedures([FromQuery] GetNumberOfProceduresQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(new { Result = result });
    }

    [HttpGet("GetTopFiveAssistantsByRatings")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetTopFiveAssistantsByRatings()
    {
        var result = await mediator.Send(new GetTopFiveAssistantsByRatingsQuery());
        return Ok(result);
    }

    [HttpGet("GetTopFiveAssistantsByAssignments")]
    public async Task<ActionResult<IEnumerable<UserProcedureCountDto>>> GetTopFiveAssistantsByAssignments()
    {
        var result = await mediator.Send(new GetTopFiveAssistantsByAssignmentsQuery());
        return Ok(result);
    }

    [HttpGet("GetTopFiveDoctors")]
    public async Task<ActionResult<IEnumerable<UserProcedureCountDto>>> GetTopFiveDoctors()
    {
        var result = await mediator.Send(new GetTopFiveDoctorsQuery());
        return Ok(result);
    }
}
