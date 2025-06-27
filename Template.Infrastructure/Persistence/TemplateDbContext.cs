
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.Schedule;

namespace Template.Infrastructure.Persistence;

public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : IdentityDbContext<User>(options)
{
	//internal DbSet<EntityType> table_name {get; set;}

	internal DbSet<Kit> Kits { get; set; }
	internal DbSet<Implant> Implants { get; set; }
	internal DbSet<Tool> Tools { get; set; }
	internal DbSet<Holiday> Holidays { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//relationships between the tables

		// Kit has many Implants
		modelBuilder.Entity<Kit>()
			.HasMany(k => k.Implants)
			.WithOne()
			.HasForeignKey(i => i.KitId);

		modelBuilder.Entity<Tool>()
			.HasOne(t => t.Kit)
			.WithMany()
			.HasForeignKey(t => t.KitId);

		modelBuilder.Entity<User>()
			.HasMany(u => u.Holidays)
			.WithOne()
			.HasForeignKey(h => h.UserId);
	}
}
