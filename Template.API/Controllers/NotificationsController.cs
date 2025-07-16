using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Notification.Command.Send;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}
