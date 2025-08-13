using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;
namespace Template.Infrastructure.Services;

public class NotificationService(TemplateDbContext dbContext) : INotificationService
{
    public async Task<List<Domain.Entities.Notifications.Notification>> GetCurrentUserNotificationsAsync(string deviceToken)
    {
        return await dbContext.Notifications.Include(n => n.Device).Where(n => n.Device.DeviceToken.Equals(deviceToken)).ToListAsync();
    }

    public async Task SaveNotificationAsync(Domain.Entities.Notifications.Notification entity)
    {
        dbContext.Notifications.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SendNotificationAsync(Domain.Entities.Notifications.Notification entity)
    {
        var device = await dbContext.Devices.FindAsync(entity.DeviceId);

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
