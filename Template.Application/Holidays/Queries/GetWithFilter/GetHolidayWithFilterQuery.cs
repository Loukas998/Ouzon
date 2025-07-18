using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Holidays.Dtos;

namespace Template.Application.Holidays.Queries.GetWithFilter
{
    public class GetHolidayWithFilterQuery(int pageNum, int pageSize, DateTime? FromDate, DateTime? ToDate, string? AssistantId) : IQuery<List<HolidayDto>>
    {
        public int PageNum { get; set; } = pageNum;
        public int PageSize { get; set; } = pageSize;
        public DateTime? FromDate { get; set; } = FromDate;
        public DateTime? ToDate { get; set; } = ToDate;
        public string? AssistantId { get; set; } = AssistantId;
    }
}
