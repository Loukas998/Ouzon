using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.Materials;

namespace Template.Domain.Repositories
{
   public interface IToolRepository :IGenericRepository<Tool>
   {
        public Task<List<Tool>> GetFilteredTools(string? name, float? width, float? height, float? thickness, int? kitId, int? categoryId, int? pageNum, int? pageSize);
   }
}