using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Devices.Commands.ChangeStatus;
using Template.Application.Notification.Queries.CurrentUserNotifications;
using Template.Domain.Repositories;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(IMediator mediator, INotificationService notificationService) : ControllerBase
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
    [HttpPost("CurrnetUserNotifications")]
    public async Task<IActionResult> GetCurrentUserNotifications([FromBody] GetCurrentUserNotificationsQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpPost("SendNotification")]
    public async Task<IActionResult> SendTestNotification([FromBody] string fcmToken)
    {
        try
        {
            await notificationService.SendTestNotificationAsync(fcmToken);
            await notificationService.SaveTestNotification(fcmToken);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    // 2- SetNotificationsAsRead
    //[HttpGet("{id}/SetNotificationsAsRead")]
    //public Task<IActionResult> SetNotificationsAsRead([FromRoute] int id)
    //{
    //    throw new NotImplementedException();
    //}
}
