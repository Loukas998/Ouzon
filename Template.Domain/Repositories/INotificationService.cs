using Template.Domain.Entities.Notifications;

namespace Template.Domain.Repositories;

public interface INotificationService
{
    public Task SaveNotificationAsync(Notification entity);

    public Task SendNotificationAsync(Notification entity);
}
