using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Commands.Create;

public class CreateKitCommandHandler(IKitRepository kitRepository, IMapper mapper,
    ILogger<CreateKitCommandHandler> logger, IFileService fileService) : ICommandHandler<CreateKitCommand, int>
{
    public async Task<Result<int>> Handle(CreateKitCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating kit: {@Kit}", request);
        var kit = mapper.Map<Kit>(request);
        if (request.Image != null)
        {
            try
            {
                kit.ImagePath = fileService.SaveFile(request.Image, "Images/Kits/", [".jpg", ".png", ".webp", ".jpeg"]);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Failure<int>([ex.Message]);
            }
        }

        var result = await kitRepository.AddAsync(kit);
        return Result.Success(result.Id);
    }
}
