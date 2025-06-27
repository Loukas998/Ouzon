using Template.Domain.Entities.Materials;

namespace Template.Application.Kits.Dtos;

public class KitDto
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public List<Implant>? Implants { get; set; } = [];
}
