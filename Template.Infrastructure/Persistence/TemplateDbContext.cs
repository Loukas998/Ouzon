
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Materials;

namespace Template.Infrastructure.Persistence;

public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : IdentityDbContext<User>(options)
{
	//internal DbSet<EntityType> table_name {get; set;}

	internal DbSet<Kit> Kits { get; set; }
	internal DbSet<Implant> Implants { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//relationships between the tables

		// Kit has many Implants
		modelBuilder.Entity<Kit>()
			.HasMany(k => k.Implants)
			.WithOne()
			.HasForeignKey(i => i.KitId);
	}
}
