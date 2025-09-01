using AutoMapper;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Commands.AssignAssistnatsToProcedure;

public class AssignAssistantsToProcedureCommandHandler(IProcedureRepository procedureRepository
    , IMapper mapper, IAccountRepository accountRepository, INotificationService notificationService) : ICommandHandler<AssignAssistantsToProcedureCommand>
{
    public async Task<Result> Handle(AssignAssistantsToProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedure = await procedureRepository.GetProcedureWithAssistants(request.ProcedureId);
        if (procedure == null)
        {
            return Result.Failure(["Entity not Found"]);
        }
        if (request.AssistantsIds.Count != procedure.NumberOfAsisstants)
        {
            return Result.Failure(["You are putting more Assistants than allowed"]);
        }
        procedure.AssistantsInProcedure.Clear();
        foreach (var assistantId in request.AssistantsIds)
        {
            var assistant = await accountRepository.GetUserAsync(assistantId, false);
            if (assistant == null)
            {
                return Result.Failure(["Assistant not Found"]);
            }
            var roleCheck = await accountRepository.UserInRoleAsync(assistantId, "AssistantDoctor");
            if (!roleCheck)
            {
                return Result.Failure(["this User isn't an assistant"]);
            }
            procedure.AssistantsInProcedure.Add(new Domain.Entities.ProcedureRelatedEntities.ProcedureAssistant()
            {
                AsisstantId = assistantId,
            });
        }
        procedure.Status = Domain.Enums.EnumProcedureStatus.IN_PROGRESS;
        await procedureRepository.UpdateAsync(procedure);

        //sending notification to each assigned assistant

        foreach (var assistantId in request.AssistantsIds)
        {

            var assistant = await accountRepository.GetUserWithDevicesAsync(assistantId);
            foreach (var device in assistant.Devices)
            {
                var assistantNotification = new Domain.Entities.Notifications.Notification
                {
                    Title = "New assignment",
                    Body = "New procedure has been assigned to you, please check the procedure's details",
                    Read = false,
                    CreatedAt = DateTime.UtcNow
                };
                assistantNotification.DeviceId = device.Id;
                if (device.DeviceToken != null && device.DeviceToken.Length > 0)
                {
                    await notificationService.SendNotificationAsync(assistantNotification);
                    await notificationService.SaveNotificationAsync(assistantNotification);
                }
            }
        }

        return Result.Success();
    }
}
