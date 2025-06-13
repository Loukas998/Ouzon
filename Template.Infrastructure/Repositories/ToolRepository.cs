using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.Materials;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ToolRepository : GenericRepository<Tool>,IToolRepository
{
    public ToolRepository(TemplateDbContext dbContext):base(dbContext)
    {
        this.dbContext = dbContext;
    }
}
