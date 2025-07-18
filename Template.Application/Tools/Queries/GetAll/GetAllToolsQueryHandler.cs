using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Application.Abstraction.Queries;
using Template.Application.Tools.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Queries.GetAll
{
    class GetAllToolsQueryHandler(IMapper mapper, IToolRepository toolRepository) : IQueryHandler<GetAllToolsQuery, IEnumerable<ToolDto>>
    {
        public async Task<Result<IEnumerable<ToolDto>>> Handle(GetAllToolsQuery request, CancellationToken cancellationToken)
        {
            var tools = await toolRepository.GetAllAsync();
            var toolsDto = mapper.Map<IEnumerable<ToolDto>>(tools);
            return Result.Success(toolsDto);
        }
    }
}
