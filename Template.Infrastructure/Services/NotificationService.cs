using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Notifications;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
namespace Template.Infrastructure.Services;

public class NotificationService(TemplateDbContext dbContext, IDeviceRepository deviceRepository) : INotificationService
{
    public async Task<List<GroupedNotification>> GetCurrentUserNotificationsAsync(string deviceToken)
    {
        var grouped = await dbContext.Notifications
            .Include(n => n.Device)
            .Where(n => n.Device.DeviceToken == deviceToken)
            .GroupBy(n => n.CreatedAt!.Value.Date)
            .Select(g => new GroupedNotification
            {
                CreatedAt = g.Key,
                Notifications = g.Select(n => new Template.Domain.Entities.Notifications.NotificationDto
                {
                    Id = n.Id,
                    CreatedAt = n.CreatedAt,
                    Title = n.Title,
                    Body = n.Body,
                    DeviceId = n.DeviceId,
                    Read = true
                }).ToList()
            })
            .ToListAsync();

        return grouped;
    }

    public async Task SaveNotificationAsync(Domain.Entities.Notifications.Notification entity)
    {
        dbContext.Notifications.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SendNotificationAsync(Domain.Entities.Notifications.Notification entity)
    {
        var device = await dbContext.Devices.FindAsync(entity.DeviceId);

        if (device != null && device.DeviceToken != null && device.DeviceToken.Length > 0)
        {
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = entity.Title,
                    Body = entity.Body,
                },
                Token = device.DeviceToken
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);

            if (!string.IsNullOrEmpty(result))
            {
                // Message was sent successfully
            }
            else
            {
                // There was an error sending the message
                throw new Exception("Error sending the message.");
            }
        }

    }

    public async Task SendTestNotificationAsync(string fcmToken)
    {
        var message = new Message()
        {
            Notification = new FirebaseAdmin.Messaging.Notification()
            {
                Title = "Test notification",
                Body = "This is a test body for test notification",
            },
            Token = fcmToken
        };

        var messaging = FirebaseMessaging.DefaultInstance;
        var result = await messaging.SendAsync(message);

        if (!string.IsNullOrEmpty(result))
        {
            // Message was sent successfully
        }
        else
        {
            // There was an error sending the message
            throw new Exception("Error sending the message.");
        }
    }

    public async Task SaveTestNotification(string fcmToken)
    {
        var notification = new Domain.Entities.Notifications.Notification()
        {
            Title = "Test notification",
            Body = "This is a test body for test notification",
            CreatedAt = DateTime.Now,
            Read = false
        };

        var deviceExist = await deviceRepository.GetDeviceByToken(fcmToken, null);
        if (deviceExist == null)
        {
            deviceExist = new Device()
            {
                DeviceToken = fcmToken,
                OptIn = true,
                LastLoggedInAt = DateTime.UtcNow,
                UserId = null
            };

            await deviceRepository.AddAsync(deviceExist);
            await dbContext.SaveChangesAsync();
        }

        deviceExist.Notifications.Add(notification);
        await dbContext.SaveChangesAsync();
    }
}
