using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Commands.Create
{
    class CreateToolCommandHandler(IToolRepository toolRepository, IMapper mapper,
        ILogger<CreateToolCommandHandler> logger, IFileService fileService) : ICommandHandler<CreateToolCommand, int>
    {
        public async Task<Result<int>> Handle(CreateToolCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Adding Tool : {request.Name}", request);
            var tool = mapper.Map<Tool>(request);
            if (request.Image != null)
            {
                tool.ImagePath = await fileService.SaveFileAsync(request.Image, "Images/Tools/", [".jpg", ".png", ".webp", ".jpeg"]);
            }
            var result = await toolRepository.AddAsync(tool);
            return Result.Success(result.Id);
        }
    }
}
