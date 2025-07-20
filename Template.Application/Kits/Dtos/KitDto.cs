using Template.Application.Implants.Dtos;
using Template.Application.Tools.Dtos;

namespace Template.Application.Kits.Dtos;

public class KitDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsMainKit { get; set; }
    public int ImplantCount { get; set; }
    public int ToolCount { get; set; }
    public List<ImplantDto>? Implants { get; set; } = [];

    public List<ToolDto>? Tools { get; set; } = [];
}
