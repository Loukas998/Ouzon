using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Tokens.Commands;
using Template.Application.Users;
using Template.Application.Users.Commands;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController(IMediator mediator,IUserContext userContext) : ControllerBase
{
    [HttpPost(Name = "RegisterUser")]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand request)
    {
        var result = await mediator.Send(request);
        if (result.Any())
        {
            return BadRequest(result);
        }
        return Ok();
    }
    [HttpPost(Name = "LoginUser")]
    public async Task<ActionResult> LoginUser(LoginUserCommand request)
    {
        var result = await mediator.Send(request);
        return Ok(result);
    }
    [HttpPost]
    [Route("RefreshToken")]
    [Authorize]
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
    [Route("CurrentUser")]
    [Authorize]
    public async Task<ActionResult<CurrentUser>> GetCurrentUser()
    {
        return Ok(userContext.GetCurrentUser());
    }
    //[HttpGet(Name ="GetUsersByRole")]
    ////public async Task<ActionResult> GetUsersByRole([FromQuery]string role)
    ////{

    ////}
}

