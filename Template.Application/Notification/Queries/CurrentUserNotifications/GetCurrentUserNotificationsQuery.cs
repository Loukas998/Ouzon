using MediatR;
using Template.Domain.Entities.Notifications;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQuery(string deviceToken) : IRequest<IEnumerable<GroupedNotification>>
{
    public string DeviceToken { get; } = deviceToken;
}
