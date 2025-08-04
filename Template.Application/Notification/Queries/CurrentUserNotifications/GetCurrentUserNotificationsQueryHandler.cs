using MediatR;
using Template.Application.Notification.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQueryHandler(INotificationService notificationService)
    : IRequestHandler<GetCurrentUserNotificationsQuery, IEnumerable<NotificationDto>>
{
    public Task<IEnumerable<NotificationDto>> Handle(GetCurrentUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
