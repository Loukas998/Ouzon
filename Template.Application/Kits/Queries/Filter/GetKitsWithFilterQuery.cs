using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Kits.Dtos;

namespace Template.Application.Kits.Queries.Filter
{
   public class GetKitsWithFilterQuery(int pageNum, int pageSize, string? brandName, bool? isMainKit, bool? hasImplants) : IQuery<List<KitDto>>
    {
        public int PageNum { get; set; } = pageNum;
        public int PageSize { get; set; } = pageSize;
        public string? BrandName { get; set; } = brandName;
        public bool? IsMainKit { get; set; } = isMainKit;
        public bool? HasImplants { get; set; } = hasImplants;
    }
}
