using AutoMapper;
using MediatR;
using Template.Domain.Entities.Notifications;
using Template.Domain.Repositories;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQueryHandler(INotificationService notificationService, IMapper mapper)
    : IRequestHandler<GetCurrentUserNotificationsQuery, IEnumerable<GroupedNotification>>
{
    public async Task<IEnumerable<GroupedNotification>> Handle(GetCurrentUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await notificationService.GetCurrentUserNotificationsAsync(request.DeviceToken);
        return notifications;
    }
}
