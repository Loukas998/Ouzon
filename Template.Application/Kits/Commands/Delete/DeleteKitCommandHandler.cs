using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Commands.Delete;

public class DeleteKitCommandHandler(ILogger<DeleteKitCommandHandler> logger,
    IKitRepository kitRepository) : ICommandHandler<DeleteKitCommand>
{
    public async Task<Result> Handle(DeleteKitCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting kit by id: {@Id}", request.Id);

        var kit = await kitRepository.GetKitDetails(request.Id);
        if (kit == null)
        {
            logger.LogError($"Could not Find Kit with Id {request.Id}");
            return Result.Failure(["Data not Found"]);
        }

        if (kit.Tools.Count() > 0)
        {
            logger.LogError("You should delete the tools in kit, then delete the kit");
            return Result.Failure(["You should delete the tools in kit, then delete the kit"]);
        }

        if (kit.Implants.Count() > 0)
        {
            logger.LogError("You should delete the implants in kit, then delete the kit");
            return Result.Failure(["You should delete the implants in kit, then delete the kit"]);
        }

        await kitRepository.DeleteAsync(kit);
        return Result.Success();
    }
}
