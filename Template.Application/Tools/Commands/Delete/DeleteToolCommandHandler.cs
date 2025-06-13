using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Commands.Delete
{
    public class DeleteToolCommandHandler(IToolRepository toolRepository, ILogger<DeleteToolCommandHandler> logger) : IRequestHandler<DeleteToolCommand>
    {
        public async Task Handle(DeleteToolCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Tool : {@Tool}", request);
            var tool = await toolRepository.FindByIdAsync(request.Id);
            if(tool == null)
            {
                throw new Exception();
            }
            await toolRepository.DeleteAsync(tool);

        }
    }
}
