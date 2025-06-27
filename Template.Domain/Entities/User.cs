using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.Schedule;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Domain.Entities
{
	public class User : IdentityUser
	{
		public string? Address { get; set; }
		public float Longtitude { get; set; }
		public float Latitude { get; set; }
		public string? ProfileImagePath { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime? UpdatedAt { get; set; }
		public List<Holiday> Holidays { get; set; } = [];

		public List<Procedure> InProcedure { get; set; }
		public List<Procedure> ProcedureFrom { get; set; }
	}
}
