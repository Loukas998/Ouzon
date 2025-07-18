using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Commands.Delete
{
    public class DeleteToolCommandHandler(IToolRepository toolRepository, ILogger<DeleteToolCommandHandler> logger) : ICommandHandler<DeleteToolCommand>
    {
        public async Task<Result> Handle(DeleteToolCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Tool : {@Tool}", request);
            var tool = await toolRepository.FindByIdAsync(request.Id);
            if(tool == null)
            {
                return Result.Failure(["Couldn't find tools"]);
            }
            await toolRepository.DeleteAsync(tool);
            return Result.Success();

        }
    }
}
