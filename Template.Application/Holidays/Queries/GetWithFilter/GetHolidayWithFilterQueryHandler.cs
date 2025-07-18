using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Holidays.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Queries.GetWithFilter
{
    public class GetHolidayWithFilterQueryHandler(IHolidayRepository holidayRepository,IMapper mapper) : IQueryHandler<GetHolidayWithFilterQuery, List<HolidayDto>>
    {
        public async Task<Result<List<HolidayDto>>> Handle(GetHolidayWithFilterQuery request, CancellationToken cancellationToken)
        {
            var holidays = await holidayRepository.GetHolidaysWithFilter(request.PageNum, request.PageSize, request.FromDate, request.ToDate, request.AssistantId);
            if(holidays.Count == 0)
            {
                return Result.Failure<List<HolidayDto>>(["Entity not Found"]);
            }
            var res = mapper.Map<List<HolidayDto>>(holidays);
            return Result.Success(res);
        }
    }
}
