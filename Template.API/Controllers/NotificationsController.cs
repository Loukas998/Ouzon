using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Devices.Commands.ChangeStatus;
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

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> ChangeDeviceNotificationStatus([FromRoute] int id, [FromBody] ChangeDeviceStatusCommand command)
    {
        command.DeviceId = id;
        await mediator.Send(command);
        return Ok();
    }

    // 1- GetCurrentUserNotifications
    [HttpGet("CurrnetUserNotifications")]
    public Task<IActionResult> GetCurrentUserNotifications()
    {
        throw new NotImplementedException();
    }
    // 2- SetNotificationsAsRead
    [HttpGet("{id}/SetNotificationsAsRead")]
    public Task<IActionResult> SetNotificationsAsRead([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}
