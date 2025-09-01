
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.Notifications;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Entities.Schedule;
using Template.Domain.Entities.Users;

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
    internal DbSet<Domain.Entities.ProcedureRelatedEntities.ProcedureAssistant> ProcedureAssistants { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Holiday> Holidays { get; set; }
    internal DbSet<Notification> Notifications { get; set; }
    internal DbSet<Device> Devices { get; set; }
    internal DbSet<Clinic> Clinics { get; set; }
    internal DbSet<Rating> Ratings { get; set; }
    internal DbSet<ProcedureImplantTool> ProcedureImplantTools { get; set; }
    internal DbSet<ProcedureImplant> ProcedureImplants { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //relationships between the tables

        //user has one or more clinics
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<User>()
        .HasIndex(u => u.NormalizedEmail)
        .IsUnique();
        modelBuilder.Entity<User>()
            .HasOne(u => u.Clinic)
            .WithOne(c => c.User)
            .HasForeignKey<Clinic>(c => c.UserId);
        // Kit has many Implants
        modelBuilder.Entity<Kit>()
            .HasMany(k => k.Implants)
            .WithOne(imp => imp.Kit)
            .HasForeignKey(i => i.KitId);

        modelBuilder.Entity<Kit>()
            .HasMany(t => t.ProceduresWithKit)
            .WithOne(tp => tp.Kit)
            .HasForeignKey(tp => tp.KitId);



        modelBuilder.Entity<Tool>()
            .HasOne(t => t.Kit)
            .WithMany(k => k.Tools)
            .HasForeignKey(t => t.KitId);



        modelBuilder.Entity<Tool>()
            .HasMany(t => t.ProceduresWithTool)
            .WithOne(tp => tp.Tool)
            .HasForeignKey(tp => tp.ToolId);

        modelBuilder.Entity<Tool>()
            .HasMany(t => t.ProcedureImplantTools)
            .WithOne(tp => tp.Tool)
            .HasForeignKey(tp => tp.ToolId);

        modelBuilder.Entity<Implant>()
            .HasMany(t => t.ProcedureImplantTools)
            .WithOne(tp => tp.Implant)
            .HasForeignKey(tp => tp.ImplantId);

        modelBuilder.Entity<Implant>()
            .HasMany(t => t.ProcedureImplants)
            .WithOne(tp => tp.Implant)
            .HasForeignKey(tp => tp.ImplantId);


        modelBuilder.Entity<Category>()
            .HasMany(c => c.Tools)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId);

        modelBuilder.Entity<Category>()
              .HasMany(c => c.Procedures)
              .WithOne(t => t.Category)
              .HasForeignKey(t => t.CategoryId);

        modelBuilder.Entity<Procedure>()
            .HasMany(p => p.AssistantsInProcedure)
            .WithOne(prass => prass.Procedure)
            .HasForeignKey(p => p.ProcedureId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(ass => ass.InProcedure)
            .WithOne(prass => prass.Asisstant)
            .HasForeignKey(prass => prass.AsisstantId);

        modelBuilder.Entity<User>()
            .HasMany(p => p.ProcedureFrom)
            .WithOne(x => x.Doctor)
            .HasForeignKey(p => p.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Procedure>()
            .HasMany(x => x.KitsInProcedure)
            .WithOne(kp => kp.Procedure)
            .HasForeignKey(kp => kp.ProcedureId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Procedure>()
            .HasMany(x => x.ToolsInProcedure)
            .WithOne(tp => tp.Procedure)
            .HasForeignKey(tp => tp.ProcedureId);


        modelBuilder.Entity<Procedure>()
            .HasMany(t => t.ProcedureImplantTools)
            .WithOne(tp => tp.Procedure)
            .HasForeignKey(tp => tp.ProcedureId);

        modelBuilder.Entity<Procedure>()
            .HasMany(t => t.ProcedureImplants)
            .WithOne(tp => tp.Procedure)
            .HasForeignKey(tp => tp.ProcedureId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Holidays)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Devices)
            .WithOne()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Device>()
            .HasMany(d => d.Notifications)
            .WithOne(n => n.Device)
            .HasForeignKey(n => n.DeviceId);

        modelBuilder.Entity<Device>()
            .HasIndex(d => d.DeviceToken)
            .IsUnique();

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Doctor)
            .WithMany(u => u.RatingsGiven)
            .HasForeignKey(r => r.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Assistant)
            .WithMany(u => u.RatingsReceived)
            .HasForeignKey(r => r.AssistantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
