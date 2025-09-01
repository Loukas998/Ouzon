using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Commands.Update;

public class UpdateImplantCommandHandler(ILogger<UpdateImplantCommandHandler> logger, IMapper mapper,
    IImplantRepository implantRepository, IFileService fileService) : ICommandHandler<UpdateImplantCommand>
{
    public async Task<Result> Handle(UpdateImplantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with id: {ImplantId}, the new product: {@Implant}",
                request.ImplantId, request);

        var implant = await implantRepository.FindByIdAsync(request.ImplantId);
        if (implant == null)
        {
            return Result.Failure(["Data not Found"]);
        }

        mapper.Map(request, implant);
        if (request.Image != null)
        {
            if (implant.ImagePath != null)
            {
                fileService.DeleteFile(implant.ImagePath);
            }
            try
            {
                implant.ImagePath = fileService.SaveFile(request.Image, "Images/Implants", [".jpg", ".png", ".webp", ".jpeg"]);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Failure([ex.Message]);
            }
        }
        await implantRepository.UpdateAsync(implant);
        return Result.Success();
    }
}
