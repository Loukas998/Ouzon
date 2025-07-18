using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Domain.Entities.Materials;

public class Implant
{
	public int Id { get; set; }
	public float Radius { get; set; }
	public float Width { get; set; }
	public float Height { get; set; }
	public int Quantity { get; set; }
	public string? Brand { get; set; }
	public string? Description { get; set; }
	public string? ImagePath { get; set; }


	public int KitId { get; set; }

	public Kit Kit { get; set; }
	public List<ProcedureImplantTool> ProcedureImplantTools { get; set; }
	public List<ProcedureImplant> ProcedureImplants{ get; set; }
}
