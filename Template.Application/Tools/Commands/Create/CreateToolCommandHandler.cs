using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Commands.Create
{
    class CreateToolCommandHandler(IToolRepository toolRepository,IMapper mapper,ILogger<CreateToolCommandHandler> logger) : ICommandHandler<CreateToolCommand, int>
    {
        public async Task<Result<int>> Handle(CreateToolCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Adding Tool : {request.Name}", request);
            var tool = mapper.Map<Tool>(request);
            var result = await toolRepository.AddAsync(tool);
            return Result.Success(result.Id);
        }
    }
}
