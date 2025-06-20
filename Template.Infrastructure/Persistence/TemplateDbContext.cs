
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Infrastructure.Persistence;

public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : IdentityDbContext<User>(options)
{
	//internal DbSet<EntityType> table_name {get; set;}

	internal DbSet<Kit> Kits { get; set; }
	internal DbSet<Implant> Implants { get; set; }
	internal DbSet<Tool> Tools { get; set; }
	internal DbSet<Procedure> Procedures { get; set; }
	internal DbSet<ProcedureKit> ProcedureKits { get; set; }
	internal DbSet<ProcedureTool> ProcedureTools { get; set; }

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


		modelBuilder.Entity<Procedure>()
			.HasOne(p => p.Assistant)
			.WithMany()
			.HasForeignKey(p => p.AssistantId);
		modelBuilder.Entity<Procedure>()
			.HasMany(x => x.KitsInProcedure)
			.WithOne()
			.HasForeignKey(x => x.ProcedureId);
			modelBuilder.Entity<Procedure>()
			.HasMany(x => x.ToolsInProcedure)
			.WithOne()
			.HasForeignKey(x => x.ProcedureId);
	}
}
