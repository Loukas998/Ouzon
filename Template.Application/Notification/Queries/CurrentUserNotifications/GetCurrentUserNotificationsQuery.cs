using MediatR;
using Template.Application.Notification.Dtos;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQuery : IRequest<IEnumerable<NotificationDto>>
{
}
