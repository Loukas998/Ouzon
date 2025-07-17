using AutoMapper;
using MediatR;
using Template.Application.Kits.Dtos;
using Template.Application.Tools.Dtos;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Tools.Queries.GetWithFilter;

public class GetToolsWithFilterQueryHandler(IToolRepository toolRepository, IMapper mapper) : IRequestHandler<GetToolsWithFilterQuery, IEnumerable<ToolDto>>
{
    public async Task<IEnumerable<ToolDto>> Handle(GetToolsWithFilterQuery request, CancellationToken cancellationToken)
    {
        var tools = await toolRepository.GetFilteredTools(
            request.Name,
            request.Width,
            request.Height,
            request.Thickness,
            request.KitId,
            request.CategoryId,
            request.PageNum,
            request.PageSize);

        var result = mapper.Map<IEnumerable<ToolDto>>(tools);
        return result;
    }
}
