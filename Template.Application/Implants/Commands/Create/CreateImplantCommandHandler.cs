using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Commands.Create;

public class CreateImplantCommandHandler(ILogger<CreateImplantCommandHandler> logger, IMapper mapper,
    IImplantRepository implantRepository, IFileService fileService) : ICommandHandler<CreateImplantCommand, int>
{
    public async Task<Result<int>> Handle(CreateImplantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating implant: {@Implant}", request);

        var implant = mapper.Map<Implant>(request);
        if (request.Image != null)
        {
            implant.ImagePath = await fileService.SaveFileAsync(request.Image, "Images/Implants/", [".jpg", ".png", ".webp", ".jpeg"]);
        }

        var result = await implantRepository.AddAsync(implant);
        return Result.Success(result.Id);
    }
}
