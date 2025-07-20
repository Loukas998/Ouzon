using Template.Application.Kits.Dtos;

namespace Template.Application.Procedure.Dtos;

public class ProcedureKitDetailsDto
{
    public IEnumerable<KitDto>? MainKits { get; set; } = [];
    public IEnumerable<KitDto>? KitsWithImplants { get; set; } = [];
    public IEnumerable<KitDto>? KitsWithoutImplants { get; set; } = [];
}
