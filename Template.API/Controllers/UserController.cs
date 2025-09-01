using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Holidays.Dtos;
using Template.Application.Holidays.Queries.GetAssistantHolidays;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Application.Procedure.Queries.AssistantProcedures;
using Template.Application.Tokens.Commands;
using Template.Application.Users;
using Template.Application.Users.Commands;
using Template.Application.Users.Commands.DeleteAccount;
using Template.Application.Users.Commands.EditProfile;
using Template.Application.Users.Dtos;
using Template.Application.Users.Queries.CurrentUser;
using Template.Application.Users.Queries.GetUsers;
using Template.Application.Users.Queries.UserDetails;
using Template.Domain.Enums;

namespace Template.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IMediator mediator, IUserContext userContext) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand request)
    {
        var result = await mediator.Send(request);
        if (result.Data != null && result.Data.Any())
        {
            return BadRequest(result);
        }
        return Ok();
    }
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginUserCommand request)
    {
        var result = await mediator.Send(request);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpPost]
    [Route("token/refresh")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestCommand request)
    {
        var response = await mediator.Send(request);
        if (response == null)
        {
            return Unauthorized();
        }
        return Ok(response);
    }
    [HttpGet]
    [Route("current")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var query = new GetCurrentUserQuery();
        var result = await mediator.Send(query);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("{Id}")]
    [ProducesResponseType(200, StatusCode = 200, Type = typeof(UserDetailedDto))]
    [Authorize]
    public async Task<ActionResult<UserDetailedDto>> GetFullUserProfile([FromRoute] string Id)
    {
        var user = await mediator.Send(new GetUserDetailsByIdQuery(Id));
        if (!user.SuccessStatus)
        {
            return BadRequest(user.Errors);
        }
        if (user.Data == null)
        {
            return NotFound();
        }
        return Ok(user.Data);
    }

    [HttpGet("holidays")]
    public async Task<ActionResult<IEnumerable<HolidayDto>>> GetAssistantHolidays()
    {
        var holidays = await mediator.Send(new GetAssistantHolidaysQuery());
        if (!holidays.SuccessStatus)
        {
            return BadRequest(holidays.Errors);
        }
        if (!holidays.Data.Any())
        {
            return NotFound();
        }
        return Ok(holidays.Data);
    }
    [HttpGet("procedures")]
    [Authorize(Roles = $"{nameof(EnumRoleNames.User)},{nameof(EnumRoleNames.AssistantDoctor)}")]
    public async Task<ActionResult<IEnumerable<ProcedureDto>>> GetAssistantProcedures()
    {
        var result = await mediator.Send(new GetAssistantProceduresQuery());
        if (!result.Data.Any())
        {
            return NotFound();
        }
        return Ok(result.Data);
    }
    [HttpGet]
    public async Task<ActionResult> GetUsers([FromQuery] string? role, string? email, string? phoneNumber, string? clinicAddress, string? clinicName)
    {
        var result = await mediator.Send(new GetUsersWithFiltersQuery(role, email, phoneNumber, clinicAddress, clinicName));
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }

    [HttpDelete("DeleteCurrentUserAccount")]
    public async Task<IActionResult> DeleteCurrentUserAccount()
    {
        try
        {
            await mediator.Send(new DeleteAccountCommand());
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateCurrentUserProfile")]
    public async Task<IActionResult> UpdateCurrentUserProfile([FromForm] EditProfileCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
