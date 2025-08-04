using MediatR;
using Template.Application.Notification.Dtos;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQuery(int deviceId) : IRequest<IEnumerable<NotificationDto>>
{
    public int DeviceId { get; } = deviceId;
}
