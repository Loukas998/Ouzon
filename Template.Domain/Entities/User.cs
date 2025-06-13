using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Entities
{
	public class User : IdentityUser
	{
		public float Longtitude { get; set; }
		public float Lattitude { get; set; }
		public string? ProfileImagePath { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime? UpdatedAt { get; set; }
	}
}
