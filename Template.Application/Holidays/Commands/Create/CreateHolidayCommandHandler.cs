using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Users;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Entities.Schedule;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Commands.Create;

public class CreateHolidayCommandHandler(ILogger<CreateHolidayCommandHandler> logger,
    IHolidayRepository holidayRepository,
    IMapper mapper,
    IUserContext userContext,
    IAccountRepository accountRepository,
    INotificationService notificationService) : ICommandHandler<CreateHolidayCommand, int>
{
    public async Task<Result<int>> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new holiday request {@Request}", request);
        var holiday = mapper.Map<Holiday>(request);
        holiday.UserId = userContext.GetCurrentUser()?.Id;
        holiday.Status = Domain.Enums.HolidayStatus.Pending;
        holiday.CreatedAt = DateTime.UtcNow;
        var result = await holidayRepository.AddAsync(holiday);

        var admins = await accountRepository.GetAdmins();

        foreach (var admin in admins)
        {
            var adminDevices = admin.Devices.ToList();
            foreach (var device in adminDevices)
            {
                var adminNotification = new Domain.Entities.Notifications.Notification
                {
                    Title = "Vacation request",
                    Body = "New vacation request has been added",
                    Read = false,
                    CreatedAt = DateTime.UtcNow,
                    DeviceId = device.Id,
                    Type = "vacation_request_added"
                };

                if (!string.IsNullOrEmpty(device.DeviceToken))
                {
                    await notificationService.SendNotificationAsync(adminNotification);
                    await notificationService.SaveNotificationAsync(adminNotification);
                }
            }
        }

        return Result.Success(result.Id);
    }
}
