using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Tools.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Queries.GetById
{
    public class GetToolByIdQueryHandler(IMapper mapper,IToolRepository toolRepository) : IQueryHandler<GetToolByIdQuery, ToolDto>
    {
        public async Task<Result<ToolDto>> Handle(GetToolByIdQuery request, CancellationToken cancellationToken)
        {
            var tool = await toolRepository.FindByIdAsync(request.Id);
            if(tool == null)
            {
                return Result.Failure<ToolDto>(["Data not Found"]);

            }
            var res = mapper.Map<ToolDto>(tool);
            return Result.Success(res);
        }
    }
}
