using MediatR;
using Template.Application.Implants.Dtos;

namespace Template.Application.Implants.Queries.GetWithFilter;

public class GetImplantsWithFilterQuery : IRequest<IEnumerable<ImplantDto>>
{
    public float? Radius { get; set; }
    public float? Width { get; set; }
    public float? Height { get; set; }
    public string? Brand { get; set; } = string.Empty;
    public int? KitId { get; set; }
    public int? PageSize { get; set; }
    public int? PageNum { get; set; }
}
