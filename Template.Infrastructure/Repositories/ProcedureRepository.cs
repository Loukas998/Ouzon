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
   public async Task<Procedure>GetWithToolsAndKitsAsync(int id)
    {
        var procedure = await dbContext.Procedures
            .Include(pro => pro.ToolsInProcedure)
            .ThenInclude(tp => tp.Tool)
            .FirstOrDefaultAsync(pro => pro.Id == id);
        return procedure;
    }

}
