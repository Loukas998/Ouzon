using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Tools.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Queries.GetById
{
    public class GetToolByIdQueryHandler(IMapper mapper,IToolRepository toolRepository) : IRequestHandler<GetToolByIdQuery, ToolDto>
    {
        public async Task<ToolDto> Handle(GetToolByIdQuery request, CancellationToken cancellationToken)
        {
            var tool = await toolRepository.FindByIdAsync(request.Id);
            if(tool == null)
            {
                throw new Exception();

            }
            return mapper.Map<ToolDto>(tool);
        }
    }
}
