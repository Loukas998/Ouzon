using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Devices.Commands.ChangeStatus;
using Template.Application.Notification.Queries.CurrentUserNotifications;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(IMediator mediator) : ControllerBase
{
    //[HttpPost]
    //public async Task<IActionResult> SendNotification([FromBody] SendNotificationCommand command)
    //{
    //    await mediator.Send(command);
    //    return Ok();
    //}

    [HttpPatch]
    public async Task<IActionResult> ChangeDeviceNotificationStatus([FromBody] ChangeDeviceStatusCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    // 1- GetCurrentUserNotifications
    [HttpGet("CurrnetUserNotifications")]
    public async Task<IActionResult> GetCurrentUserNotifications([FromBody] GetCurrentUserNotificationsQuery query)
    {
        return Ok(await mediator.Send(query));
    }
    // 2- SetNotificationsAsRead
    //[HttpGet("{id}/SetNotificationsAsRead")]
    //public Task<IActionResult> SetNotificationsAsRead([FromRoute] int id)
    //{
    //    throw new NotImplementedException();
    //}
}
