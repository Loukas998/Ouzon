using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Tools.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Queries.GetAll
{
    class GetAllToolsQueryHandler(IMapper mapper, IToolRepository toolRepository) : IRequestHandler<GetAllToolsQuery, IEnumerable<ToolDto>>
    {
        public async Task<IEnumerable<ToolDto>> Handle(GetAllToolsQuery request, CancellationToken cancellationToken)
        {
            var tools = await toolRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ToolDto>>(tools);
        }
    }
}
