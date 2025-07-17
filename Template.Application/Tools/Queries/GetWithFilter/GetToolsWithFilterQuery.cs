using MediatR;
using Template.Application.Tools.Dtos;

namespace Template.Application.Tools.Queries.GetWithFilter;

public class GetToolsWithFilterQuery : IRequest<IEnumerable<ToolDto>>
{
    public string Name { get; set; } = string.Empty;
    public float? Width { get; set; }
    public float? Height { get; set; }
    public float? Thickness { get; set; }
    public int? KitId { get; set; }
    public int? CategoryId { get; set; }
    public int? PageNum { get; set; }
    public int? PageSize { get; set; }
}
