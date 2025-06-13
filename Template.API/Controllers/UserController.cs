using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Users.Commands;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController(IMediator mediator) : ControllerBase
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
}

