using MediatR;
using Template.Application.Notification.Dtos;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQuery(string deviceToken) : IRequest<IEnumerable<NotificationDto>>
{
    public string DeviceToken { get; } = deviceToken;
}
