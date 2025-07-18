using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Application.Tools.Dtos;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Commands.Update;

public class UpdateToolCommandHandler(IToolRepository toolRepository, IMapper mapper,ILogger<UpdateToolCommandHandler> logger) : ICommandHandler<UpdateToolCommand>
{

    public async Task <Result> Handle(UpdateToolCommand request, CancellationToken cancellationToken)
    {
        var tool= await toolRepository.FindByIdAsync(request.Id);
        if (tool == null) 
        {
            return Result.Failure(["Data not Found"]);
        }
        mapper.Map(request, tool);
        await toolRepository.UpdateAsync(tool);
        return Result.Success();

    }
}
