using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Entities.Materials;

public class Kit
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? ImagePath { get; set; }

	public List<Implant>? Implants { get; set; }

	public List<Tool> Tools { get; set; }
}
