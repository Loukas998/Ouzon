using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Domain.Entities.Materials;

public class Kit
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? ImagePath { get; set; }
	public bool IsMainKit { get; set; } = false;

	public List<Implant>? Implants { get; set; }

	public List<Tool> Tools { get; set; }

	[JsonIgnore]
	public List<ProcedureKit> ProceduresWithKit { get; set; }
}
