﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Domain.Repositories
{
    public interface IProcedureRepository : IGenericRepository<Procedure>
    {

        Task<Procedure> GetWithToolsAndKitsAsync(int id);
        Task<List<Procedure>> GetFilteredProcedures(int pageSize, int pageNum, string? DoctorId, string? AssistantId);
    }
}
