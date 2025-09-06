using AutoMapper;
using MediatR;
using Template.Application.Users;
using Template.Domain.Entities.Users;
using Template.Domain.Repositories;

namespace Template.Application.Ratings.Commands.Rate;

public class AddRatingToAssistantCommandHandler(IRatingsRepository ratingsRepository,
    IMapper mapper,
    IAccountRepository accountRepository,
    INotificationService notificationService,
    IUserContext userContext) : IRequestHandler<AddRatingToAssistantCommand>
{
    public async Task Handle(AddRatingToAssistantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var rating = mapper.Map<Rating>(request);
            var currentUser = userContext.GetCurrentUser();
            rating.DoctorId = currentUser.Id;
            await ratingsRepository.AddAsync(rating);

            var assistant = await accountRepository.GetUserWithDevicesAsync(request.AssistantId!);
            foreach (var device in assistant.Devices)
            {
                var assistantNotification = new Domain.Entities.Notifications.Notification
                {
                    Title = "Doctor Rate",
                    Body = $"New rating has been added to your profile by: {currentUser.UserName}",
                    Read = false,
                    CreatedAt = DateTime.UtcNow,
                    DeviceId = device.Id,
                    Type = "assistants_assignment"
                };

                if (!string.IsNullOrEmpty(device.DeviceToken))
                {
                    await notificationService.SendNotificationAsync(assistantNotification);
                    await notificationService.SaveNotificationAsync(assistantNotification);
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}
