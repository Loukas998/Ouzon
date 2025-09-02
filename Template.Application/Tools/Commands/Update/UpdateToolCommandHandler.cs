using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Commands.Update;

public class UpdateToolCommandHandler(IToolRepository toolRepository, IMapper mapper,
    ILogger<UpdateToolCommandHandler> logger, IFileService fileService) : ICommandHandler<UpdateToolCommand>
{

    public async Task<Result> Handle(UpdateToolCommand request, CancellationToken cancellationToken)
    {
        var tool = await toolRepository.FindByIdAsync(request.Id);
        if (tool == null)
        {
            return Result.Failure(["Data not Found"]);
        }
        mapper.Map(request, tool);
        if (request.Image != null)
        {
            if (tool.ImagePath != null)
            {
                fileService.DeleteFile(tool.ImagePath);
            }
            try
            {
                tool.ImagePath = await fileService.SaveFileAsync(request.Image, "Images/Tools", [".jpg", ".png", ".webp", ".jpeg"]);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Failure([ex.Message]);
            }
        }
        await toolRepository.UpdateAsync(tool);
        return Result.Success();

    }
}
