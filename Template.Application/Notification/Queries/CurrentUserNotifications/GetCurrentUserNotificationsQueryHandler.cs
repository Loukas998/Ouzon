using AutoMapper;
using MediatR;
using Template.Application.Notification.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Notification.Queries.CurrentUserNotifications;

public class GetCurrentUserNotificationsQueryHandler(INotificationService notificationService, IMapper mapper)
    : IRequestHandler<GetCurrentUserNotificationsQuery, IEnumerable<NotificationDto>>
{
    public async Task<IEnumerable<NotificationDto>> Handle(GetCurrentUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await notificationService.GetCurrentUserNotificationsAsync(request.DeviceId);
        var notificationsDtos = mapper.Map<IEnumerable<NotificationDto>>(notifications);
        return notificationsDtos;
    }
}
