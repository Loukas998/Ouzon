using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Commands.ChangeStatus;

public class ChangeHolidayStatusCommandHandler(ILogger<ChangeHolidayStatusCommandHandler> logger,
    IHolidayRepository holidayRepository, IAccountRepository accountRepository,
    INotificationService notificationService,
    IMapper mapper) : ICommandHandler<ChangeHolidayStatusCommand>
{
    public async Task<Result> Handle(ChangeHolidayStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Changing holiday status of id: {Id} with data: {@Request}", request.HolidayId, request);

        var holiday = await holidayRepository.FindByIdAsync(request.HolidayId);
        if (holiday == null)
        {
            return Result.Failure(["Entity not found"]);
        }

        var oldStatus = holiday.Status;
        mapper.Map(request, holiday);
        await holidayRepository.SaveChangesAsync();

        var assistant = await accountRepository.GetUserWithDevicesAsync(holiday.UserId);
        foreach (var device in assistant.Devices)
        {
            var assistantNotification = new Domain.Entities.Notifications.Notification
            {
                Title = "Vacation status updated",
                Body = $"Your vacation request's status has been changed from, {oldStatus} to {request.NewStatus}",
                Read = false,
                CreatedAt = DateTime.UtcNow,
                DeviceId = device.Id,
                Type = "vacation_request_status_changed"
            };
            assistantNotification.DeviceId = device.Id;
            if (!string.IsNullOrEmpty(device.DeviceToken))
            {
                await notificationService.SendNotificationAsync(assistantNotification);
                await notificationService.SaveNotificationAsync(assistantNotification);
            }
        }

        return Result.Success();
    }
}
