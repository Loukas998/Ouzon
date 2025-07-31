using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Ratings.Commands.Rate;
using Template.Domain.Enums;

namespace Template.API.Controllers;

[ApiController, Route("api/[controller]")]
public class RatingsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = nameof(EnumRoleNames.User))]
    public async Task<IActionResult> RateAssistant(AddRatingToAssistantCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}
