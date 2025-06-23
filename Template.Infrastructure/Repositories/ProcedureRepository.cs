using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ProcedureRepository :GenericRepository<Procedure>,IProcedureRepository
{
    public TemplateDbContext dbContext;
    public IToolRepository toolRepository;
    public IKitRepository kitRepository;
    public ProcedureRepository(TemplateDbContext dbContext, IToolRepository toolRepository, IKitRepository kitRepository) : base(dbContext)
    {
        this.dbContext = dbContext;
        this.toolRepository = toolRepository;
        this.kitRepository = kitRepository;
    }
    //public async Task<List<int>> AddToolsToProcedure(int procedureId, List<int> toolIds)
    //{
    //    try { 
    //   var procedure = await dbContext.Procedures.FirstOrDefaultAsync(x => x.Id == procedureId);
    //        if (procedure == null)
    //        {
    //        throw new Exception();
    //        }
    //        var procedureTools = new List<ProcedureTool>();
    //        var procedureToolsIds = new List<int>();
    //        foreach(var id in toolIds)
    //        {
    //            var tool = await toolRepository.FindByIdAsync(id);
    //            if(tool == null)
    //            {
    //                throw new Exception();
    //            }
    //            var proTool = new ProcedureTool()
    //            {
    //                ProcedureId = procedureId,
    //                ToolId = id,
    //            };
    //            procedureTools.Add(proTool);
    //            procedureToolsIds.Add(proTool.Id);
    //        }
    //        await dbContext.ProcedureTools.AddRangeAsync(procedureTools);
    //        await dbContext.SaveChangesAsync();
    //        return procedureToolsIds;
    //}
    //    catch(Exception ex)
    //    {
    //        logger.LogError(ex, "Something Went Wrong");
    //        throw;
    //    }
    //}
    //public async Task<List<int>> AddKitsToProcedure(int procedureId, List<int> kitsIds)
    //{
    //    try
    //    {
    //        var procedure = await dbContext.Procedures.FirstOrDefaultAsync(x => x.Id == procedureId);
    //        if (procedure == null)
    //        {
    //            throw new Exception();
    //        }
    //        var procedureKits = new List<ProcedureKit>();
    //        var procedureKitsIds = new List<int>();
    //        foreach (var id in kitsIds)
    //        {
    //            var tool = await kitRepository.FindByIdAsync(id);
    //            if (tool == null)
    //            {
    //                throw new Exception();
    //            }
    //            var proTool = new ProcedureKit()
    //            {
    //                ProcedureId = procedureId,
    //                KitId = id,
    //            };
    //            procedureKits.Add(proTool);
    //            procedureKitsIds.Add(proTool.Id);
    //        }
    //        await dbContext.ProcedureKits.AddRangeAsync();
    //        await dbContext.SaveChangesAsync();
    //        return procedureKitsIds;
    //    }
    //    catch (Exception ex)
    //    {
    //        logger.LogError(ex, "Something Went Wrong");
    //        throw;
    //    }
    //}

}
