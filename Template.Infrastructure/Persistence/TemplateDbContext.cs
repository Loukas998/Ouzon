
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Entities.Schedule;

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
	internal DbSet<Category> Categories{ get; set; }
	internal DbSet<Holiday> Holidays { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//relationships between the tables

		// Kit has many Implants
		modelBuilder.Entity<Kit>()
			.HasMany(k => k.Implants)
			.WithOne(imp=>imp.Kit)
			.HasForeignKey(i => i.KitId);

        modelBuilder.Entity<Kit>()
            .HasMany(t => t.ProceduresWithKit)
            .WithOne(tp => tp.Kit)
            .HasForeignKey(tp => tp.KitId);



        modelBuilder.Entity<Tool>()
			.HasOne(t => t.Kit)
			.WithMany(k=>k.Tools)
			.HasForeignKey(t => t.KitId);



		modelBuilder.Entity<Tool>()
			.HasMany(t => t.ProceduresWithTool)
			.WithOne(tp => tp.Tool)
			.HasForeignKey(tp => tp.ToolId);



        modelBuilder.Entity<Category>()
	.HasMany(c => c.Tools)
	.WithOne(t => t.Category)
	.HasForeignKey(t => t.CategoryId);

		modelBuilder.Entity<Category>()
	  .HasMany(c => c.Procedures)
	  .WithOne(t => t.Category)
	  .HasForeignKey(t => t.CategoryId);

        modelBuilder.Entity<Procedure>()
			.HasOne(p => p.Assistant)
			.WithMany(ass=>ass.InProcedure)
			.HasForeignKey(p => p.AssistantId)
			.OnDelete(DeleteBehavior.NoAction);


		modelBuilder.Entity<Procedure>()
		.HasOne(p => p.Doctor)
		.WithMany(doc=>doc.ProcedureFrom)
		.HasForeignKey(p => p.DoctorId)
		.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<Procedure>()
			.HasMany(x => x.KitsInProcedure)
			.WithOne(kp => kp.Procedure)
			.HasForeignKey(kp => kp.ProcedureId);


			modelBuilder.Entity<Procedure>()
			.HasMany(x => x.ToolsInProcedure)
			.WithOne(tp=>tp.Procedure)
			.HasForeignKey(tp=> tp.ProcedureId);




		modelBuilder.Entity<User>()
			.HasMany(u => u.Holidays)
			.WithOne()
			.HasForeignKey(h => h.UserId);
	}
}
